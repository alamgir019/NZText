namespace NZ.Shared.Contracts.Leave;

public record EmployeePendingLeaveResult(
    string EmployeeId,
    string LeaveCode,
    decimal PendingDays);
