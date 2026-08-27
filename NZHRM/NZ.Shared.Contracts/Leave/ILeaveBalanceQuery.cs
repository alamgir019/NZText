namespace NZ.Shared.Contracts.Leave;

public interface ILeaveBalanceQuery
{
    Task<IReadOnlyList<EmployeeLeaveBalanceResult>> GetAllBalancesAsync(
        string employeeId,
        CancellationToken cancellationToken = default);

    Task<LeaveBalanceResult?> GetBalanceAsync(
        string employeeId,
        string leaveTypeCode,
        int year,
        CancellationToken cancellationToken = default);
}

public record LeaveBalanceResult(
    string EmployeeId,
    string LeaveTypeCode,
    decimal TotalEntitled,
    decimal TotalUsed,
    decimal Balance);
