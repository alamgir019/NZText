using Microsoft.EntityFrameworkCore;
using NZ.Leave.Application.Interfaces.Repositories;
using NZ.Shared.Contracts.Leave;
using NZ.Leave.Application.LeaveEncashmentRequests.Dto;
using NZ.Leave.Domain.Entities;
using NZ.Leave.Infrastructure.Persistence;

namespace NZ.Leave.Infrastructure.Repositories
{
    public class LeaveEncashmentRequestRepository : ILeaveEncashmentRequestRepository
    {
        private readonly LeaveDbContext _context;
        private readonly ILeaveBalanceQuery _leaveBalanceQuery;

        public LeaveEncashmentRequestRepository(LeaveDbContext context, ILeaveBalanceQuery leaveBalanceQuery)
        {
            _context = context;
            _leaveBalanceQuery = leaveBalanceQuery;
        }

        public async Task<string> CreateAsync(LeaveEncashmentRequestDto dto, CancellationToken cancellationToken = default)
        {
            var leaveType = await _context.LevLeaveTypes
                .FirstOrDefaultAsync(lt => lt.LeaveCode == dto.LeaveType, cancellationToken);

            if (leaveType == null)
                throw new KeyNotFoundException($"Leave type {dto.LeaveType} not found");

            var entity = new LevLeaveEncashment
            {
                EmployeeId = dto.EmployeeId,
                LeaveTypeId = leaveType.Id,
                EncashDate = dto.EncashDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc),
                EncashDays = dto.EncashDays,
                Reason = dto.Reason,
                Instalment = dto.Instalment,
                Status = dto.Status,
                FromDate = dto.FromDate,
                ToDate = dto.ToDate,
                CreatedBy = dto.CreatedBy ?? string.Empty
            };

            _context.LevLeaveEncashments.Add(entity);
            // Create an initial encashment history record for the new encashment request
            var history = new LevLeaveEncashmentHistory
            {
                EncashmentId = entity.Id,
                WorkflowStepNo = 1,
                ApproverId = dto.CreatedBy,
                ActionTaken = "Submitted",
                Remarks = dto.Reason ?? string.Empty,
                CreatedBy = dto.CreatedBy ?? string.Empty
            };

            _context.LevLeaveEncashmentHistories.Add(history);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task<LeaveEncashmentRequestDto?> GetByIdAsync(string requestId, CancellationToken cancellationToken = default)
        {
            var entity = await _context.LevLeaveEncashments
                .Include(a => a.Employee)
                .Include(a => a.LeaveType)
                .FirstOrDefaultAsync(a => a.Id == requestId, cancellationToken);

            if (entity == null) return null;

            // Get forwarded info from history (most recent history record)
            var history = await _context.LevLeaveEncashmentHistories
                .Where(h => h.EncashmentId == entity.Id)
                .OrderByDescending(h => h.CreatedOn)
                .FirstOrDefaultAsync(cancellationToken);

            // Get leave balance / accrued info for this employee and leave type
            var balances = await _leaveBalanceQuery.GetAllBalancesAsync(new List<string> { entity.EmployeeId }, cancellationToken);
            var balance = balances.FirstOrDefault(b => string.Equals(b.LeaveCode, entity.LeaveType?.LeaveCode, StringComparison.OrdinalIgnoreCase));

            var dto = Map(entity);
            dto.ForwardedBy = history?.ApproverId ?? history?.CreatedBy ?? dto.ForwardedBy;
            dto.ForwardedDate = history?.CreatedOn ?? dto.ForwardedDate;
            dto.EarnedLeaveBalance = balance?.ClosingBalance ?? 0m;
            dto.EarnedLeaveAccruedThisYear = balance?.EarnedLeaveAccrued ?? 0m;

            return dto;
        }

        public async Task<(List<LeaveEncashmentRequestDto> Items, int Total)> GetAllAsync(
            string? status,
            string? instalment,
            int page,
            int size,
            CancellationToken cancellationToken = default)
        {
            var query = _context.LevLeaveEncashments
                .Include(a => a.Employee)
                .ThenInclude(a => a.Employment != null ? a.Employment.Department : null)
                .Include(a => a.LeaveType)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(a => a.Status == status);

            if (!string.IsNullOrWhiteSpace(instalment))
                query = query.Where(a => a.Instalment == instalment);

            var total = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(a => a.CreatedOn)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);

            var encashmentIds = items.Select(i => i.Id).ToList();
            var employeeIds = items.Select(i => i.EmployeeId).Distinct().ToList();

            // fetch latest history per encashment
            var histories = await _context.LevLeaveEncashmentHistories
                .Where(h => encashmentIds.Contains(h.EncashmentId))
                .OrderByDescending(h => h.CreatedOn)
                .ToListAsync(cancellationToken);

            // fetch leave balances for employees
            var balances = await _leaveBalanceQuery.GetAllBalancesAsync(employeeIds, cancellationToken);

            var result = new List<LeaveEncashmentRequestDto>(items.Count);

            foreach (var item in items)
            {
                var dto = Map(item);

                var hist = histories.FirstOrDefault(h => h.EncashmentId == item.Id);
                if (hist != null)
                {
                    dto.ForwardedBy = hist.ApproverId ?? hist.CreatedBy ?? dto.ForwardedBy;
                    dto.ForwardedDate = hist.CreatedOn;
                }

                var bal = balances.FirstOrDefault(b => b.EmployeeId == item.EmployeeId && string.Equals(b.LeaveCode, item.LeaveType?.LeaveCode, StringComparison.OrdinalIgnoreCase));
                if (bal != null)
                {
                    dto.EarnedLeaveBalance = bal.ClosingBalance;
                    dto.EarnedLeaveAccruedThisYear = bal.EarnedLeaveAccrued;
                }

                result.Add(dto);
            }

            return (result, total);
        }

        public async Task UpdateAsync(LeaveEncashmentRequestDto dto, CancellationToken cancellationToken = default)
        {
            var entity = await _context.LevLeaveEncashments
                .FirstOrDefaultAsync(a => a.Id == dto.RequestId, cancellationToken);

            if (entity == null)
                throw new KeyNotFoundException($"Leave request {dto.RequestId} not found");

            var leaveType = await _context.LevLeaveTypes
                .FirstOrDefaultAsync(lt => lt.LeaveCode == dto.LeaveType, cancellationToken);

            if (leaveType == null)
                throw new KeyNotFoundException($"Leave type {dto.LeaveType} not found");

            entity.EmployeeId = dto.EmployeeId;
            entity.LeaveTypeId = leaveType.Id;
            entity.EncashDate = dto.EncashDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
            entity.EncashDays = dto.EncashDays;
            entity.Reason = dto.Reason;
            entity.UpdatedBy = dto.ModifiedBy ?? entity.UpdatedBy;
            entity.Status = dto.Status;
            // Create an initial encashment history record for the new encashment request
            var history = new LevLeaveEncashmentHistory
            {
                EncashmentId = entity.Id,
                WorkflowStepNo = 1,
                ApproverId = dto.CreatedBy,
                ActionTaken = dto.Status,
                Remarks = dto.Reason ?? string.Empty,
                CreatedBy = dto.CreatedBy ?? string.Empty
            };

            _context.LevLeaveEncashmentHistories.Add(history);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(string requestId, CancellationToken cancellationToken = default)
        {
            var entity = await _context.LevLeaveEncashments
                .FirstOrDefaultAsync(a => a.Id == requestId, cancellationToken);

            if (entity == null)
                return;

            _context.LevLeaveEncashments.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private static LeaveEncashmentRequestDto Map(LevLeaveEncashment entity) => new LeaveEncashmentRequestDto
        {
            RequestId = entity.Id,
            EmployeeId = entity.EmployeeId,
            EmployeeCode = entity.Employee?.EmployeeCode ?? string.Empty,
            EmployeeName = entity.Employee?.EmployeeName ?? string.Empty,
            Department = entity.Employee?.Employment?.Department?.DepartmentName,
            LeaveType = entity.LeaveType?.LeaveCode ?? string.Empty,
            EncashDate = entity.EncashDate.HasValue ? DateOnly.FromDateTime(entity.EncashDate.Value) : default,
            EncashDays = entity.EncashDays,
            Reason = entity.Reason ?? string.Empty,
            Instalment = entity.Instalment ?? string.Empty,
            Status = entity.Status ?? string.Empty,
            FromDate = entity.FromDate,
            ToDate = entity.ToDate,
            ForwardedBy = entity.CreatedBy,
            ForwardedDate = entity.CreatedOn,
            EarnedLeaveBalance = 0m,
            EarnedLeaveAccruedThisYear = 0m,
        };
    }
}
