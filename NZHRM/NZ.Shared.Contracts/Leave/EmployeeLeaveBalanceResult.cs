namespace NZ.Shared.Contracts.Leave;

public record EmployeeLeaveBalanceResult(
	string LeaveCode,
	string LeaveName,
	decimal ClosingBalance);
