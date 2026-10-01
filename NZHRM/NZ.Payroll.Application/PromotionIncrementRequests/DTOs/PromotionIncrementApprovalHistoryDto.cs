namespace NZ.Payroll.Application.PromotionIncrementRequests.DTOs;

public class PromotionIncrementApprovalHistoryDto
{
	public string Id { get; set; } = string.Empty;
	public int StepNo { get; set; }
	public string StepName { get; set; } = string.Empty;
	public string Action { get; set; } = string.Empty;
	public string FromStatus { get; set; } = string.Empty;
	public string ToStatus { get; set; } = string.Empty;
	public string ActionBy { get; set; } = string.Empty;
	public DateTime ActionOn { get; set; }
	public string? Remarks { get; set; }
}
