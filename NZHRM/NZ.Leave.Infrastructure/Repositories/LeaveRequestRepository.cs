using Microsoft.EntityFrameworkCore;
using NZ.Leave.Application.Interfaces.Repositories;
using NZ.Leave.Application.LeaveRequests.Dto;
using NZ.Leave.Domain.Entities;
using NZ.Leave.Infrastructure.Persistence;

namespace NZ.Leave.Infrastructure.Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly LeaveDbContext _context;

        public LeaveRequestRepository(LeaveDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateAsync(LeaveRequestDto dto, CancellationToken cancellationToken = default)
        {
            var leaveType = await _context.LevLeaveTypes
                .FirstOrDefaultAsync(lt => lt.LeaveCode == dto.LeaveType, cancellationToken);

            if (leaveType == null)
                throw new KeyNotFoundException($"Leave type {dto.LeaveType} not found");

            var entity = new LevLeaveApplication
            {
                EmployeeId = dto.EmployeeId,
                LeaveTypeId = leaveType.Id,
                FromDate = dto.FromDate,
                ToDate = dto.ToDate,
                TotalDays = dto.TotalDays,
                LeaveReason = dto.Reason,
                LeaveStatus = dto.Status,
                ApplicationDate = dto.CreatedDate ?? DateTime.UtcNow,
                ForwardedBy = dto.ForwardedBy,
                ForwardedDate = dto.ForwardedDate,
                CreatedBy = dto.CreatedBy ?? string.Empty
            };

            _context.LevLeaveApplications.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task<LeaveRequestDto?> GetByIdAsync(string requestId, CancellationToken cancellationToken = default)
        {
            var entity = await _context.LevLeaveApplications
                .Include(a => a.Employee)
                .Include(a => a.LeaveType)
                .FirstOrDefaultAsync(a => a.Id == requestId, cancellationToken);

            return entity == null ? null : Map(entity);
        }

        public async Task<(List<LeaveRequestDto> Items, int Total)> GetAllAsync(
            string? status,
            int page,
            int size,
            CancellationToken cancellationToken = default)
        {
            var query = _context.LevLeaveApplications
                .Include(a => a.Employee)
                .Include(a => a.LeaveType)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(a => a.LeaveStatus == status);

            var total = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(a => a.CreatedOn)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);

            return (items.Select(Map).ToList(), total);
        }

        public async Task UpdateAsync(LeaveRequestDto dto, CancellationToken cancellationToken = default)
        {
            var entity = await _context.LevLeaveApplications
                .FirstOrDefaultAsync(a => a.Id == dto.RequestId, cancellationToken);

            if (entity == null)
                throw new KeyNotFoundException($"Leave request {dto.RequestId} not found");

            var leaveType = await _context.LevLeaveTypes
                .FirstOrDefaultAsync(lt => lt.LeaveCode == dto.LeaveType, cancellationToken);

            if (leaveType == null)
                throw new KeyNotFoundException($"Leave type {dto.LeaveType} not found");

            entity.LeaveTypeId = leaveType.Id;
            entity.FromDate = dto.FromDate;
            entity.ToDate = dto.ToDate;
            entity.TotalDays = dto.TotalDays;
            entity.LeaveReason = dto.Reason;
            entity.ForwardedBy = dto.ForwardedBy;
            entity.ForwardedDate = dto.ForwardedDate;
            entity.UpdatedBy = dto.ModifiedBy ?? entity.UpdatedBy;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(string requestId, CancellationToken cancellationToken = default)
        {
            var entity = await _context.LevLeaveApplications
                .FirstOrDefaultAsync(a => a.Id == requestId, cancellationToken);

            if (entity == null)
                return;

            _context.LevLeaveApplications.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsForEmployeeAsync(string employeeId, DateOnly fromDate, DateOnly toDate, string? excludeRequestId = null, CancellationToken cancellationToken = default)
        {
            var query = _context.LevLeaveApplications
                .Where(a => a.EmployeeId == employeeId
                    && a.FromDate <= toDate
                    && a.ToDate >= fromDate);

            if (!string.IsNullOrEmpty(excludeRequestId))
                query = query.Where(a => a.Id != excludeRequestId);

            return await query.AnyAsync(cancellationToken);
        }

        private static LeaveRequestDto Map(LevLeaveApplication entity) => new LeaveRequestDto
        {
            RequestId = entity.Id,
            EmployeeId = entity.EmployeeId,
            EmployeeName = entity.Employee?.EmployeeName ?? string.Empty,
            LeaveType = entity.LeaveType?.LeaveCode ?? string.Empty,
            FromDate = entity.FromDate,
            ToDate = entity.ToDate,
            TotalDays = entity.TotalDays,
            Reason = entity.LeaveReason ?? string.Empty,
            Status = entity.LeaveStatus ?? string.Empty,
            CreatedBy = entity.CreatedBy,
            CreatedDate = entity.CreatedOn,
            ModifiedBy = entity.UpdatedBy,
            ModifiedDate = entity.UpdatedOn,
            ForwardedBy = entity.ForwardedBy,
            ForwardedDate = entity.ForwardedDate
        };
    }
}
