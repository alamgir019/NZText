namespace NZ.Payroll.Application.PayIncrementHistories.DTOs;

public class PayIncrementHistoryDto
{
	public string Id { get; set; } = string.Empty;
	public string EmployeeId { get; set; } = string.Empty;
	public DateOnly? EffectiveDate { get; set; }
	public decimal? OldGrossSalary { get; set; }
	public decimal? NewGrossSalary { get; set; }
	public decimal? IncrementAmount { get; set; }
	public decimal? IncrementPercent { get; set; }
	public string? ApprovedBy { get; set; }
	public DateTime? ApprovalDate { get; set; }
	public string? ForwardedBy { get; set; }
	public DateTime? ForwardDate { get; set; }
	public string? IncrementType { get; set; }
}
