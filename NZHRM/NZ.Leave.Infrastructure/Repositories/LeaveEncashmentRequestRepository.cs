using Microsoft.EntityFrameworkCore;
using NZ.Leave.Application.Interfaces.Repositories;
using NZ.Leave.Application.LeaveEncashmentRequests.Dto;
using NZ.Leave.Domain.Entities;
using NZ.Leave.Infrastructure.Persistence;

namespace NZ.Leave.Infrastructure.Repositories
{
    public class LeaveEncashmentRequestRepository : ILeaveEncashmentRequestRepository
    {
        private readonly LeaveDbContext _context;

        public LeaveEncashmentRequestRepository(LeaveDbContext context)
        {
            _context = context;
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
                ForwardedBy = dto.ForwardedBy,
                ForwardedDate = dto.ForwardedDate,
                FromDate = dto.FromDate,
                ToDate = dto.ToDate,
                CreatedBy = dto.CreatedBy ?? string.Empty
            };

            _context.LevLeaveEncashments.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task<LeaveEncashmentRequestDto?> GetByIdAsync(string requestId, CancellationToken cancellationToken = default)
        {
            var entity = await _context.LevLeaveEncashments
                .Include(a => a.Employee)
                .Include(a => a.LeaveType)
                .FirstOrDefaultAsync(a => a.Id == requestId, cancellationToken);

            return entity == null ? null : Map(entity);
        }

        public async Task<(List<LeaveEncashmentRequestDto> Items, int Total)> GetAllAsync(
            string? status,
            int page,
            int size,
            CancellationToken cancellationToken = default)
        {
            var query = _context.LevLeaveEncashments
                .Include(a => a.Employee)
                .Include(a => a.LeaveType)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(a => a.Instalment == status);

            var total = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(a => a.CreatedOn)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);

            return (items.Select(Map).ToList(), total);
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
            entity.EncashDate = dto.EncashDate.ToDateTime(TimeOnly.MinValue);
            entity.EncashDays = dto.EncashDays;
            entity.Reason = dto.Reason;
            entity.ForwardedBy = dto.ForwardedBy;
            entity.ForwardedDate = dto.ForwardedDate;
            entity.UpdatedBy = dto.ModifiedBy ?? entity.UpdatedBy;

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
            EmployeeName = entity.Employee?.EmployeeName ?? string.Empty,
            LeaveType = entity.LeaveType?.LeaveCode ?? string.Empty,
            EncashDate = entity.EncashDate.HasValue ? DateOnly.FromDateTime(entity.EncashDate.Value) : default,
            EncashDays = entity.EncashDays,
            Reason = entity.Reason ?? string.Empty,
            Instalment = entity.Instalment ?? string.Empty,
            Status = entity.Status ?? string.Empty,
            FromDate = entity.FromDate,
            ToDate = entity.ToDate,
            CreatedBy = entity.CreatedBy,
            CreatedDate = entity.CreatedOn,
            ModifiedBy = entity.UpdatedBy,
            ModifiedDate = entity.UpdatedOn,
            ForwardedBy = entity.ForwardedBy,
            ForwardedDate = entity.ForwardedDate
        };
    }
}
