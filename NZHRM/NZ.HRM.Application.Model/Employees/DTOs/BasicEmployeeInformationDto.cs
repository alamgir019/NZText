namespace NZ.HRM.Application.Model.Employees.DTOs;

public class BasicEmployeeInformationDto
{
	public string EmployeeCode { get; set; } = string.Empty;
	public string EmployeeName { get; set; } = string.Empty;
	public string? EmployeeNameBangla { get; set; }
    public List<EmployeeLeaveBalanceDto> Leaves { get; set; } = new List<EmployeeLeaveBalanceDto>();
    public string EmployeeId { get; set; } = string.Empty;
}
