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
            .FirstOrDefaultAsync(cancellationToken);

        if (currentLeaveYear == null)
            return Array.Empty<EmployeeLeaveBalanceResult>();

        // AccrualMonth is typically formatted as "YYYYMM" or "YYYY-MM"
        var yearStringPrefix = currentLeaveYear.LeaveYearValue.ToString();

        // 1. Calculate sum of accruals grouped by EmployeeId and LeaveTypeId for the current year
        var accruedSumQuery = from accrual in _context.LevLeaveAccruals.AsNoTracking()
                              where employeeIds.Contains(accrual.EmployeeId)
                                 && accrual.AccrualMonth.StartsWith(yearStringPrefix)
                              group accrual by new { accrual.EmployeeId, accrual.LeaveTypeId } into g
                              select new
                              {
                                  g.Key.EmployeeId,
                                  g.Key.LeaveTypeId,
                                  TotalAccrued = g.Sum(x => x.AccruedDays)
                              };

        var accruedSums = await accruedSumQuery.ToListAsync(cancellationToken);

        // 2. Fetch the base leave types and existing balances
        var rows = await (
            from leaveType in _context.LevLeaveTypes.AsNoTracking()
            where leaveType.IsActive
            join balance in _context.LevLeaveBalances.AsNoTracking()
                .Where(balance => employeeIds.Contains(balance.EmployeeId) && balance.YearId == currentLeaveYear.Id)
                on leaveType.Id equals balance.LeaveTypeId into balances
            from balance in balances.DefaultIfEmpty()
            orderby leaveType.LeaveCode
            select new
            {
                EmployeeId = balance == null ? null : balance.EmployeeId,
                leaveType.Id,
                leaveType.LeaveCode,
                leaveType.LeaveName,
                ClosingBalance = balance == null ? 0m : balance.ClosingBalance,
                EarnedLeave = balance == null ? 0m : balance.EarnedLeave
            })
            .ToListAsync(cancellationToken);

        // 3. Combine base balances with actual calculated accruals dynamically across your employee group
        var results = new List<EmployeeLeaveBalanceResult>();

        foreach (var employeeId in employeeIds)
        {
            foreach (var row in rows.Where(r => r.EmployeeId == employeeId))
            {
                // If we got a row matching this employee, use it; otherwise create empty placeholder defaults
                var isRowForThisEmployee = row.EmployeeId == employeeId;
                var closingBalance = isRowForThisEmployee ? row.ClosingBalance : 0m;
                var earnedLeave = isRowForThisEmployee ? row.EarnedLeave : 0m;

                var accruedDays = accruedSums
                    .FirstOrDefault(x => x.EmployeeId == employeeId && x.LeaveTypeId == row.Id)
                    ?.TotalAccrued ?? 0m;

                results.Add(new EmployeeLeaveBalanceResult(
                    employeeId,
                    row.LeaveCode,
                    row.LeaveName,
                    closingBalance,
                    earnedLeave,
                    accruedDays));
            }
        }

        return results;
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