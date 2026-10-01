namespace NZ.HRM.Domain.Constants;

public static class PromotionIncrementStatuses
{
	public const string Draft = "DRAFT";
	public const string PendingDirectorApproval = "PENDING_DIRECTOR_APPROVAL";
	public const string ApprovedByDirector = "APPROVED_BY_DIRECTOR";
	public const string PendingEmployeeMovementCell = "PENDING_EMPLOYEE_MOVEMENT_CELL";
	public const string ReviewedByEmployeeMovementCell = "REVIEWED_BY_EMPLOYEE_MOVEMENT_CELL";
	public const string PendingHrBranchManager = "PENDING_HR_BRANCH_MANAGER";
	public const string PendingCeoApproval = "PENDING_CEO_APPROVAL";
	public const string Approved = "APPROVED";
	public const string Rejected = "REJECTED";

	public static readonly IReadOnlyCollection<string> Closed = new[] { Approved, Rejected };

	public static readonly IReadOnlyCollection<string> All = new[]
	{
		Draft,
		PendingDirectorApproval,
		ApprovedByDirector,
		PendingEmployeeMovementCell,
		ReviewedByEmployeeMovementCell,
		PendingHrBranchManager,
		PendingCeoApproval,
		Approved,
		Rejected
	};
}
