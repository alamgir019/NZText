using Microsoft.EntityFrameworkCore;
using NZ.Leave.Infrastructure.Persistence;
using NZ.Shared.Contracts.Leave;

namespace NZ.Leave.Infrastructure.Services;

public class LeaveBalanceQueryService : ILeaveBalanceQuery
{
    private readonly LeaveDbContext _context;

    public LeaveBalanceQueryService(LeaveDbContext context)
    {
        _context = context;
    }

    public async Task<LeaveBalanceResult?> GetBalanceAsync(
        string employeeId,
        string leaveTypeCode,
        int year,
        CancellationToken cancellationToken = default)
    {
        var leaveType = await _context.LevLeaveTypes
            .FirstOrDefaultAsync(lt => lt.LeaveCode == leaveTypeCode, cancellationToken);

        if (leaveType == null) return null;

        var balance = await _context.LevLeaveBalances
            .FirstOrDefaultAsync(
                lb => lb.EmployeeId == employeeId
                   && lb.LeaveTypeId == leaveType.Id,
                cancellationToken);

        if (balance == null) return null;

        var totalEntitled = balance.OpeningBalance + balance.EarnedLeave + balance.AdjustedLeave;
        var totalUsed = balance.AvailedLeave + balance.EncashedLeave;

        return new LeaveBalanceResult(
            employeeId,
            leaveTypeCode,
            totalEntitled,
            totalUsed,
            balance.ClosingBalance);
    }
}
