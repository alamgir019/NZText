namespace NZ.Payroll.Application.PayIncrementHistories.DTOs;

public class PerIncrementRequestDto
{
	public string Id { get; set; } = string.Empty;
	public string PayIncHistId { get; set; } = string.Empty;
	public string? ApprovedBy { get; set; }
	public DateTime? ApprovalDate { get; set; }
	public DateTime CreatedOn { get; set; }
	public string CreatedBy { get; set; } = string.Empty;
	public DateTime UpdatedOn { get; set; }
	public string UpdatedBy { get; set; } = string.Empty;
	public bool IsActive { get; set; }
}
