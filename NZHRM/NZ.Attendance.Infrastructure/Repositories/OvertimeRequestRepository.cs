using Microsoft.EntityFrameworkCore;
using NZ.Attendance.Application.Interfaces.Repositories;
using NZ.Attendance.Application.OvertimeRequests.Dto;
using NZ.HRM.Domain.Entities;
using NZ.Attendance.Infrastructure.Persistence;
using NZ.Attendance.Application.OvertimeRequests.Commands.ApproveOvertimeRequest;

namespace NZ.Attendance.Infrastructure.Repositories
{
    public class OvertimeRequestRepository : IOvertimeRequestRepository
    {
        private readonly AttendanceDbContext _context;

        public OvertimeRequestRepository(AttendanceDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateAsync(OvertimeRequestDto dto)
        {
            // Create a request id and insert one item row per employee with duplicated request header fields.
            var requestId = NZ.HRM.Domain.Helper.IdentityGenerator.Next();

            var items = new List<AttOtRequestItem>();
            foreach (var emp in dto.Employees)
            {
                items.Add(BuildItem(requestId, dto, emp));
            }

            _context.AttOtRequestItems.AddRange(items);
            await _context.SaveChangesAsync();
            return requestId;
        }

        // Builds an AttOtRequestItem for the given employee under the given request, validating OT hours format.
        private static AttOtRequestItem BuildItem(string requestId, OvertimeRequestDto dto, OvertimeEmployeeDto emp)
        {
            if (!TimeSpan.TryParseExact(emp.OTHours, @"hh\:mm", null, out var ts))
                throw new FormatException($"Invalid OT hours format for employee {emp.EmployeeId}");

            return new AttOtRequestItem
            {
                RequestId = requestId,
                CurrentShiftId = dto.CurrentShiftId,
                OtDate = DateOnly.FromDateTime(dto.OTDate),
                DepartmentId = dto.DepartmentId,
                Reason = dto.Reason,
                EmployeeId = emp.EmployeeId,
                OtHours = ts,
                Status = "Submitted",
                SubmittedBy = emp.SubmittedBy ?? string.Empty,
                SubmittedOn = DateTime.UtcNow
            };
        }

        public async Task AddEmployeeAsync(string overtimeRequestId, OvertimeEmployeeDto dto)
        {
            // Add a new item row under the given request id, reusing the same header fields as the existing request
            var exists = await _context.AttOtRequestItems.AnyAsync(e => e.RequestId == overtimeRequestId && e.EmployeeId == dto.EmployeeId);
            if (exists)
                throw new InvalidOperationException("Employee already exists in OT request");

            var header = await _context.AttOtRequestItems
                .Where(i => i.RequestId == overtimeRequestId)
                .FirstOrDefaultAsync();
            if (header == null)
                throw new KeyNotFoundException($"Overtime request {overtimeRequestId} not found");

            var headerDto = new OvertimeRequestDto
            {
                Id = overtimeRequestId,
                CurrentShiftId = header.CurrentShiftId,
                OTDate = header.OtDate.ToDateTime(new TimeOnly(0, 0)),
                DepartmentId = header.DepartmentId,
                Reason = header.Reason
            };

            var item = BuildItem(overtimeRequestId, headerDto, dto);

            _context.AttOtRequestItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<OvertimeRequestDto?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            // Gather all item rows for the request id and aggregate into a request dto
            var items = await _context.AttOtRequestItems
                .Where(i => i.RequestId == id)
                .ToListAsync(cancellationToken);

            if (items == null || items.Count == 0)
                return null;

            var first = items.First();
            var dto = new OvertimeRequestDto
            {
                Id = first.RequestId,
                CurrentShiftId = first.CurrentShiftId,
                OTDate = first.OtDate.ToDateTime(new TimeOnly(0, 0)),
                DepartmentId = first.DepartmentId,
                Reason = first.Reason
            };

            foreach (var e in items)
            {
                dto.Employees.Add(new OvertimeEmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    OTHours = e.OtHours.ToString(@"hh\:mm"),
                    Status = e.Status,
                    ItemId = e.Id
                });
            }

            return dto;
        }

        public async Task<(List<OvertimeRequestDto> Items, int Total)> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string? shiftId = null,
            string? departmentId = null,
            DateTime? from = null,
            DateTime? to = null,
            string? status = null,
            CancellationToken cancellationToken = default)
        {
            // Work over item rows, apply filters, then group by RequestId to return one dto per request
            var itemsQuery = _context.AttOtRequestItems.AsQueryable();

            if (!string.IsNullOrWhiteSpace(shiftId)) itemsQuery = itemsQuery.Where(r => r.CurrentShiftId == shiftId);
            if (!string.IsNullOrWhiteSpace(departmentId)) itemsQuery = itemsQuery.Where(r => r.DepartmentId == departmentId);
            if (from.HasValue) itemsQuery = itemsQuery.Where(r => r.OtDate >= DateOnly.FromDateTime(from.Value));
            if (to.HasValue) itemsQuery = itemsQuery.Where(r => r.OtDate <= DateOnly.FromDateTime(to.Value));
            if (!string.IsNullOrWhiteSpace(status)) itemsQuery = itemsQuery.Where(r => r.Status == status);

            var grouped = itemsQuery
                .GroupBy(i => i.RequestId)
                .Select(g => new
                {
                    RequestId = g.Key,
                    Latest = g.OrderByDescending(x => x.CreatedOn).FirstOrDefault(),
                    Items = g
                });

            var total = await grouped.CountAsync(cancellationToken);

            var page = await grouped
                .OrderByDescending(g => g.Latest.CreatedOn)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var list = new List<OvertimeRequestDto>();
            foreach (var group in page)
            {
                var first = group.Latest;
                var dto = new OvertimeRequestDto
                {
                    Id = group.RequestId,
                    CurrentShiftId = first?.CurrentShiftId ?? string.Empty,
                    OTDate = first != null ? first.OtDate.ToDateTime(new TimeOnly(0, 0)) : DateTime.MinValue,
                    DepartmentId = first?.DepartmentId ?? string.Empty,
                    Reason = first?.Reason ?? string.Empty,
                };

                foreach (var e in group.Items)
                {
                    dto.Employees.Add(new OvertimeEmployeeDto
                    {
                        EmployeeId = e.EmployeeId,
                        OTHours = e.OtHours.ToString(@"hh\:mm"),
                        Status = e.Status,
                        ItemId = e.Id
                    });
                }

                list.Add(dto);
            }

            return (list, total);
        }

        public async Task ApproveAsync(List<ApproveOvertimeRequestCommand> commands, CancellationToken cancellationToken = default)
        {
            if (commands == null || !commands.Any())
                throw new ArgumentException("No commands provided");

            foreach (var command in commands)
            {
                var items = await _context.AttOtRequestItems.FirstOrDefaultAsync(i => i.Id == command.OvertimeRequestId, cancellationToken);
                if (items == null) throw new KeyNotFoundException($"Overtime request {command.OvertimeRequestId} not found");
                items.Status = command.Approved ? "Approved" : "Rejected";
                items.ApprovedBy = command.ApprovedBy ?? string.Empty;
                items.ApprovalDate = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeApprovalAsync(string itemId, string approvedBy, bool approved = true)
        {
            var item = await _context.AttOtRequestItems.FirstOrDefaultAsync(i => i.Id == itemId);
            if (item == null) throw new KeyNotFoundException($"Overtime request item {itemId} not found");
            item.Status = approved ? "Approved" : "Rejected";
            item.ApprovedBy = approvedBy ?? string.Empty;
            item.ApprovalDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task<List<EmployeeByShiftDto>> GetEmployeesByShiftAndDepartmentAsync(string shiftId, string departmentId, CancellationToken cancellationToken = default)
        {
            // Get distinct employee ids assigned to the shift via the shift roster
            var employeeIds = await _context.AttShiftRosters
                .Where(r => r.ShiftId == shiftId)
                .Select(r => r.EmployeeId)
                .Distinct()
                .ToListAsync(cancellationToken);

            // Join employment + employee master + designation + department
            var result = await (
                from emp in _context.HrmEmployeeEmployments
                where (employeeIds.Contains(emp.EmployeeId) || (!employeeIds.Contains(emp.EmployeeId) && emp.ShiftId == shiftId))
                && emp.DepartmentId == departmentId
                join master in _context.HrmEmployeeMasters on emp.EmployeeId equals master.Id
                join designation in _context.MstDesignations on emp.DesignationId equals designation.Id into designationJoin
                from designation in designationJoin.DefaultIfEmpty()
                join department in _context.MstDepartments on emp.DepartmentId equals department.Id into departmentJoin
                from department in departmentJoin.DefaultIfEmpty()
                select new EmployeeByShiftDto
                {
                    EmployeeId = master.Id,
                    EmployeeName = master.EmployeeName,
                    EmployeeCode = master.EmployeeCode,
                    DesignationId = emp.DesignationId,
                    DesignationName = designation != null ? designation.DesignationName : string.Empty,
                    DepartmentId = emp.DepartmentId,
                    DepartmentName = department != null ? department.DepartmentName : string.Empty
                }
            ).ToListAsync(cancellationToken);

            return result;
        }
    }
}

