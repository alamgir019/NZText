using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Constants;

namespace NZ.HRM.Domain.Entities;

[Table("promotion_increment_request", Schema = "payroll")]
public class PayPromotionIncrementRequest : BaseEntityWithSortOrder
{
	public const int ReasonMaxLength = 500;

	[Required]
	public string EmployeeId { get; set; } = string.Empty;

	public string? DepartmentId { get; set; }
	public string? SectionId { get; set; }
	public string? CurrentDesignationId { get; set; }

	[Required]
	public string ProposedDesignationId { get; set; } = string.Empty;

	public string? CurrentGradeId { get; set; }

	[Required]
	public string ProposedGradeId { get; set; } = string.Empty;

	public decimal CurrentGrossSalary { get; set; }
	public decimal IncrementPercent { get; set; }
	public decimal IncrementAmount { get; set; }
	public decimal NewGrossSalary { get; set; }
	public DateOnly EffectiveFrom { get; set; }

	[MaxLength(ReasonMaxLength)]
	public string? Reason { get; set; }

	[Required]
	public string Status { get; set; } = PromotionIncrementStatuses.Draft;

	public int CurrentStepNo { get; set; } = PromotionIncrementApprovalSteps.ProductionFloor;

	public ICollection<PayPromotionIncrementApprovalHistory> ApprovalHistories { get; set; }
		= new List<PayPromotionIncrementApprovalHistory>();

	[ForeignKey(nameof(EmployeeId))] public HrmEmployeeMaster? Employee { get; set; }
	[ForeignKey(nameof(DepartmentId))] public MstDepartment? Department { get; set; }
	[ForeignKey(nameof(SectionId))] public MstSection? Section { get; set; }
	[ForeignKey(nameof(CurrentDesignationId))] public MstDesignation? CurrentDesignation { get; set; }
	[ForeignKey(nameof(ProposedDesignationId))] public MstDesignation? ProposedDesignation { get; set; }
	[ForeignKey(nameof(CurrentGradeId))] public MstGrade? CurrentGrade { get; set; }
	[ForeignKey(nameof(ProposedGradeId))] public MstGrade? ProposedGrade { get; set; }

	public static PayPromotionIncrementRequest Create(
		string employeeId,
		string? departmentId,
		string? sectionId,
		string? currentDesignationId,
		string proposedDesignationId,
		string? currentGradeId,
		string proposedGradeId,
		decimal currentGrossSalary,
		decimal incrementPercent,
		decimal incrementAmount,
		DateOnly effectiveFrom,
		string? reason,
		string createdBy)
	{
		if (string.IsNullOrWhiteSpace(employeeId))
			throw new ArgumentException("Employee ID is required");

		if (string.IsNullOrWhiteSpace(proposedDesignationId))
			throw new ArgumentException($"Proposed designation is required for employee '{employeeId}'");

		if (string.IsNullOrWhiteSpace(proposedGradeId))
			throw new ArgumentException($"New grade is required for employee '{employeeId}'");

		if (string.Equals(currentDesignationId, proposedDesignationId, StringComparison.OrdinalIgnoreCase) &&
			string.Equals(currentGradeId, proposedGradeId, StringComparison.OrdinalIgnoreCase))
			throw new ArgumentException(
				$"Proposed designation or grade must differ from the current one for employee '{employeeId}'");

		if (currentGrossSalary <= 0)
			throw new ArgumentException($"Current gross salary is not set for employee '{employeeId}'");

		if (incrementPercent <= 0 || incrementPercent > 100)
			throw new ArgumentException($"Increment percent must be greater than 0 and at most 100 for employee '{employeeId}'");

		if (incrementAmount <= 0)
			throw new ArgumentException($"Increment amount must be greater than 0 for employee '{employeeId}'");

		if (effectiveFrom == default)
			throw new ArgumentException($"Effective date is required for employee '{employeeId}'");

		var trimmedReason = string.IsNullOrWhiteSpace(reason) ? null : reason.Trim();
		if (trimmedReason?.Length > ReasonMaxLength)
			throw new ArgumentException($"Reason must not exceed {ReasonMaxLength} characters");

		return new PayPromotionIncrementRequest
		{
			EmployeeId = employeeId,
			DepartmentId = departmentId,
			SectionId = sectionId,
			CurrentDesignationId = currentDesignationId,
			ProposedDesignationId = proposedDesignationId,
			CurrentGradeId = currentGradeId,
			ProposedGradeId = proposedGradeId,
			CurrentGrossSalary = currentGrossSalary,
			IncrementPercent = incrementPercent,
			IncrementAmount = incrementAmount,
			NewGrossSalary = currentGrossSalary + incrementAmount,
			EffectiveFrom = effectiveFrom,
			Reason = trimmedReason,
			Status = PromotionIncrementStatuses.Draft,
			CurrentStepNo = PromotionIncrementApprovalSteps.ProductionFloor,
			CreatedBy = createdBy,
			UpdatedBy = createdBy,
			IsActive = true
		};
	}

	public PayPromotionIncrementApprovalHistory ForwardToDirector(string forwardedBy, string? remarks)
	{
		if (string.IsNullOrWhiteSpace(forwardedBy))
			throw new ArgumentException("Forwarded by is required");

		if (!string.Equals(Status, PromotionIncrementStatuses.Draft, StringComparison.OrdinalIgnoreCase))
			throw new InvalidOperationException(
				$"Promotion increment request {Id} is already {Status} and cannot be forwarded to Director");

		return RecordTransition(
			PromotionIncrementApprovalActions.ForwardedToDirector,
			PromotionIncrementStatuses.PendingDirectorApproval,
			PromotionIncrementApprovalSteps.Director,
			forwardedBy,
			remarks);
	}

	public PayPromotionIncrementApprovalHistory ForwardToEmployeeMovementCell(string forwardedBy, string? remarks)
	{
		if (string.IsNullOrWhiteSpace(forwardedBy))
			throw new ArgumentException("Forwarded by is required");

		if (!string.Equals(Status, PromotionIncrementStatuses.ApprovedByDirector, StringComparison.OrdinalIgnoreCase))
			throw new InvalidOperationException(
				$"Promotion increment request {Id} is {Status}; only requests approved by Director can be forwarded to Attendance & Workforce Movement Section");

		return RecordTransition(
			PromotionIncrementApprovalActions.ForwardedToEmployeeMovementCell,
			PromotionIncrementStatuses.PendingEmployeeMovementCell,
			PromotionIncrementApprovalSteps.EmployeeMovementCell,
			forwardedBy,
			remarks);
	}

	public PayPromotionIncrementApprovalHistory ForwardToHrBranchManager(string forwardedBy, string? remarks)
	{
		if (string.IsNullOrWhiteSpace(forwardedBy))
			throw new ArgumentException("Forwarded by is required");

		if (!string.Equals(Status, PromotionIncrementStatuses.ReviewedByEmployeeMovementCell, StringComparison.OrdinalIgnoreCase))
			throw new InvalidOperationException(
				$"Promotion increment request {Id} is {Status}; only requests reviewed by Employee Movement Cell can be forwarded to HR Branch Manager");

		return RecordTransition(
			PromotionIncrementApprovalActions.ForwardedToHrBranchManager,
			PromotionIncrementStatuses.PendingHrBranchManager,
			PromotionIncrementApprovalSteps.HrBranchManager,
			forwardedBy,
			remarks);
	}

	public PayPromotionIncrementApprovalHistory ForwardToCeo(string forwardedBy, string? remarks)
	{
		if (string.IsNullOrWhiteSpace(forwardedBy))
			throw new ArgumentException("Forwarded by is required");

		if (!string.Equals(Status, PromotionIncrementStatuses.PendingHrBranchManager, StringComparison.OrdinalIgnoreCase))
			throw new InvalidOperationException(
				$"Promotion increment request {Id} is {Status}; only requests pending with HR Branch Manager can be forwarded to CEO");

		return RecordTransition(
			PromotionIncrementApprovalActions.ForwardedToCeo,
			PromotionIncrementStatuses.PendingCeoApproval,
			PromotionIncrementApprovalSteps.Ceo,
			forwardedBy,
			remarks);
	}

	private PayPromotionIncrementApprovalHistory RecordTransition(
		string action,
		string toStatus,
		int nextStepNo,
		string actionBy,
		string? remarks)
	{
		var actionOn = DateTime.UtcNow;
		var history = new PayPromotionIncrementApprovalHistory
		{
			PromotionIncrementRequestId = Id,
			StepNo = CurrentStepNo,
			StepName = PromotionIncrementApprovalSteps.GetName(CurrentStepNo),
			Action = action,
			FromStatus = Status,
			ToStatus = toStatus,
			ActionBy = actionBy,
			ActionOn = actionOn,
			Remarks = string.IsNullOrWhiteSpace(remarks) ? null : remarks.Trim(),
			CreatedBy = actionBy,
			UpdatedBy = actionBy
		};

		Status = toStatus;
		CurrentStepNo = nextStepNo;
		UpdatedBy = actionBy;
		UpdatedOn = actionOn;
		ApprovalHistories.Add(history);

		return history;
	}
}
