namespace NZ.Payroll.Application.PayIncrementHistories.DTOs;

public class UpdatePayIncrementHistoryResponseDto
{
	public string Id { get; set; } = string.Empty;
	public string RequestId { get; set; } = string.Empty;
	public string EmployeeId { get; set; } = string.Empty;
	public DateOnly? EffectiveDate { get; set; }
	public decimal? OldGrossSalary { get; set; }
	public decimal? NewGrossSalary { get; set; }
	public decimal? IncrementAmount { get; set; }
	public decimal? IncrementPercent { get; set; }
	public string? IncrementType { get; set; }
	public string ApprovedBy { get; set; } = string.Empty;
	public DateTime? ApprovalDate { get; set; }
}
