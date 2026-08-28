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

    public async Task<IReadOnlyList<EmployeeLeaveBalanceResult>> GetAllBalancesAsync(
        List<string> employeeIds,
        CancellationToken cancellationToken = default)
    {
        var currentLeaveYear = await _context.LevLeaveYears
            .AsNoTracking()
            .Where(year => year.IsCurrentYear)
            .Select(year => (int?)year.LeaveYearValue)
            .FirstOrDefaultAsync(cancellationToken);

        if (!currentLeaveYear.HasValue)
            return Array.Empty<EmployeeLeaveBalanceResult>();

        var rows = await (
            from leaveType in _context.LevLeaveTypes.AsNoTracking()
            where leaveType.Status
            join balance in _context.LevLeaveBalances.AsNoTracking()
                .Where(balance => employeeIds.Contains(balance.EmployeeId) && balance.YearId == currentLeaveYear.Value)
                on leaveType.Id equals balance.LeaveTypeId into balances
            from balance in balances.DefaultIfEmpty()
            orderby leaveType.LeaveCode
            select new
            {
                balance.EmployeeId,
                leaveType.LeaveCode,
                leaveType.LeaveName,
                ClosingBalance = balance == null ? 0m : balance.ClosingBalance
            })
            .ToListAsync(cancellationToken);

        return rows
            .Select(row => new EmployeeLeaveBalanceResult(
                row.EmployeeId,
                row.LeaveCode,
                row.LeaveName,
                row.ClosingBalance))
            .ToList();
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
