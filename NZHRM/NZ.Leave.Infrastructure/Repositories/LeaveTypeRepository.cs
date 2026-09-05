using Microsoft.EntityFrameworkCore;
using NZ.Leave.Application.Interfaces.Repositories;
using NZ.Leave.Application.LeaveTypes.Dto;
using NZ.Leave.Infrastructure.Persistence;

namespace NZ.Leave.Infrastructure.Repositories
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly LeaveDbContext _context;

        public LeaveTypeRepository(LeaveDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeaveTypeDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.LevLeaveTypes
                .Select(lt => new LeaveTypeDto
                {
                    Id = lt.Id,
                    LeaveCode = lt.LeaveCode,
                    LeaveName = lt.LeaveName,
                    LeaveCategory = lt.LeaveCategory,
                    AnnualEntitlement = lt.AnnualEntitlement,
                    Encashable = lt.Encashable,
                    CarryForwardAllowed = lt.CarryForwardAllowed,
                    MaxCarryForwardDays = lt.MaxCarryForwardDays,
                    ApprovalRequired = lt.ApprovalRequired,
                    Status = lt.IsActive
                })
                .ToListAsync(cancellationToken);
        }
    }
}
