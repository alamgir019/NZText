namespace NZ.Payroll.Application.PayIncrementHistories.DTOs;

public class PayIncrementHistoryWithRequestsDto
{
	public string Id { get; set; } = string.Empty;
	public string EmployeeId { get; set; } = string.Empty;
	public string EmployeeCode { get; set; } = string.Empty;
	public string EmployeeName { get; set; } = string.Empty;
	public string DepartmentName { get; set; } = string.Empty;
	public string SectionName { get; set; } = string.Empty;
	public DateOnly? EffectiveDate { get; set; }
	public decimal? OldGrossSalary { get; set; }
	public decimal? NewGrossSalary { get; set; }
	public decimal? IncrementAmount { get; set; }
	public decimal? IncrementPercent { get; set; }
	public string? IncrementType { get; set; }
	public string Status { get; set; } = string.Empty;
	public List<PerIncrementRequestDto> Requests { get; set; } = new();
}
