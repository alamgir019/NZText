namespace NZ.Shared.Contracts.Leave;

public record EmployeeLeaveBalanceResult(
	string EmployeeId,
	string LeaveCode,
	string LeaveName,
	decimal ClosingBalance,
	decimal EarnedLeave,
	decimal EarnedLeaveAccrued);
