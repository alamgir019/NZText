namespace NZ.Shared.Contracts.Leave;

public interface ILeaveBalanceQuery
{
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
