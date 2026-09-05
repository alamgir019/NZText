namespace NZ.HRM.Application.Model.Employees.DTOs;

public class EmployeeLeaveBalanceDto
{
	public string LeaveCode { get; set; } = string.Empty;
	public string LeaveName { get; set; } = string.Empty;
	public decimal ClosingBalance { get; set; }
    public decimal EarnedLeave { get; set; }
    public decimal AccruedLeave { get; set; }
}
