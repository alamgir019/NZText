using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.HRM.Domain.Entities;

[Table("learner_confirmation_approval_history", Schema = "hrm")]
public class HrmLearnerConfirmationApprovalHistory : BaseEntity
{
    [Required]
    public string LearnerConfirmationRequestId { get; set; } = string.Empty;

    public int StepNo { get; set; }

    [MaxLength(100)]
    public string StepName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Action { get; set; } = string.Empty;

    [MaxLength(50)]
    public string FromStatus { get; set; } = string.Empty;

    [MaxLength(50)]
    public string ToStatus { get; set; } = string.Empty;

    public string ActionBy { get; set; } = string.Empty;
    public DateTime ActionOn { get; set; }

    public string? Remarks { get; set; }

    [ForeignKey(nameof(LearnerConfirmationRequestId))]
    public HrmLearnerConfirmationRequest? LearnerConfirmationRequest { get; set; }
}