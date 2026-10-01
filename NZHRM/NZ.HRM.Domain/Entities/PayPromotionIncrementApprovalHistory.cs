using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.HRM.Domain.Entities;

[Table("promotion_increment_approval_history", Schema = "payroll")]
public class PayPromotionIncrementApprovalHistory : BaseEntity
{
	[Required]
	public string PromotionIncrementRequestId { get; set; } = string.Empty;

	public int StepNo { get; set; }

	[MaxLength(50)]
	public string StepName { get; set; } = string.Empty;

	[MaxLength(50)]
	public string Action { get; set; } = string.Empty;

	[MaxLength(50)]
	public string FromStatus { get; set; } = string.Empty;

	[MaxLength(50)]
	public string ToStatus { get; set; } = string.Empty;

	public string ActionBy { get; set; } = string.Empty;
	public DateTime ActionOn { get; set; }

	[MaxLength(PayPromotionIncrementRequest.ReasonMaxLength)]
	public string? Remarks { get; set; }

	[ForeignKey(nameof(PromotionIncrementRequestId))]
	public PayPromotionIncrementRequest? PromotionIncrementRequest { get; set; }
}
