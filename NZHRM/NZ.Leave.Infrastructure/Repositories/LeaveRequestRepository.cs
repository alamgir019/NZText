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
                CreatedBy = dto.CreatedBy ?? string.Empty
            };

            _context.LevLeaveApplications.Add(entity);

            // Create an initial approval history record for the new leave application
            var history = new LevLeaveApprovalHistory
            {
                LeaveApplicationId = entity.Id,
                WorkflowStepNo = 1,
                ApproverId = dto.CreatedBy,
                ActionTaken = "Submitted",
                Remarks = dto.Reason ?? string.Empty,
                CreatedBy = dto.CreatedBy ?? string.Empty
            };

            _context.LevLeaveApprovalHistories.Add(history);

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

            // Map base DTOs
            var dtos = items.Select(Map).ToList();

            // Fetch available leave balances for the listed employees and attach to DTOs
            var employeeIds = dtos.Select(d => d.EmployeeId).Distinct().ToList();
            if (employeeIds.Any())
            {
                var balances = await _context.LevLeaveBalances
                    .Include(b => b.LeaveType)
                    .Where(b => employeeIds.Contains(b.EmployeeId))
                    .ToListAsync(cancellationToken);

                var balancesByEmployee = balances
                    .GroupBy(b => b.EmployeeId)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(b => new LeaveBalanceDto
                        {
                            LeaveTypeId = b.LeaveTypeId,
                            LeaveTypeName = b.LeaveType?.LeaveName ?? string.Empty,
                            OpeningBalance = b.OpeningBalance,
                            EarnedLeave = b.EarnedLeave,
                            AvailedLeave = b.AvailedLeave,
                            AdjustedLeave = b.AdjustedLeave,
                            EncashedLeave = b.EncashedLeave,
                            ClosingBalance = b.ClosingBalance
                        }).ToList()
                    );

                foreach (var dto in dtos)
                {
                    if (balancesByEmployee.TryGetValue(dto.EmployeeId, out var list))
                        dto.AvailableLeaves = [.. list.Where(x => x.LeaveTypeId == dto.LeaveTypeId)];
                }
            }

            return (dtos, total);
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
            // set updated metadata
            entity.UpdatedBy = dto.ApprovedBy ?? dto.CreatedBy ?? entity.UpdatedBy;
            entity.UpdatedOn = DateTime.UtcNow;

            // Insert approval history record for this update
            var nextStepNo = await _context.LevLeaveApprovalHistories
                .Where(h => h.LeaveApplicationId == entity.Id)
                .CountAsync(cancellationToken) + 1;

            var history = new LevLeaveApprovalHistory
            {
                LeaveApplicationId = entity.Id,
                WorkflowStepNo = nextStepNo,
                ApproverId = dto.ApprovedBy ?? dto.CreatedBy,
                ActionTaken = dto.ApproveStatus ?? dto.Status ?? "Updated",
                Remarks = dto.Reason ?? string.Empty,
                CreatedBy = dto.ApprovedBy ?? dto.CreatedBy ?? string.Empty
            };

            _context.LevLeaveApprovalHistories.Add(history);

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
            EmployeeCode = entity.Employee?.EmployeeCode ?? string.Empty,
            EmployeeName = entity.Employee?.EmployeeName ?? string.Empty,
            LeaveType = entity.LeaveType?.LeaveName ?? string.Empty,
            LeaveTypeId = entity.LeaveType?.Id ?? string.Empty,
            FromDate = entity.FromDate,
            ToDate = entity.ToDate,
            TotalDays = entity.TotalDays,
            Reason = entity.LeaveReason ?? string.Empty,
            Status = entity.LeaveStatus ?? string.Empty,
            CreatedBy = entity.CreatedBy,
            CreatedDate = entity.CreatedOn,
            ApprovedBy = entity.UpdatedBy,
            ApprovedDate = entity.UpdatedOn
        };
    }
}
