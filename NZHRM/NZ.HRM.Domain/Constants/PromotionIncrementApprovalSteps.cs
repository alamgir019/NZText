namespace NZ.HRM.Domain.Constants;

public static class PromotionIncrementApprovalSteps
{
	public const int ProductionFloor = 1;
	public const int Director = 2;
	public const int EmployeeMovementCell = 3;
	public const int HrBranchManager = 4;
	public const int Ceo = 5;

	public static string GetName(int stepNo) => stepNo switch
	{
		ProductionFloor => "PRODUCTION_FLOOR",
		Director => "DIRECTOR",
		EmployeeMovementCell => "EMPLOYEE_MOVEMENT_CELL",
		HrBranchManager => "HR_BRANCH_MANAGER",
		Ceo => "CEO",
		_ => throw new ArgumentOutOfRangeException(nameof(stepNo), stepNo, "Unknown promotion increment approval step")
	};
}

public static class PromotionIncrementApprovalActions
{
	public const string ForwardedToDirector = "FORWARDED_TO_DIRECTOR";
	public const string ApprovedByDirector = "APPROVED_BY_DIRECTOR";
	public const string ForwardedToEmployeeMovementCell = "FORWARDED_TO_EMPLOYEE_MOVEMENT_CELL";
	public const string ReviewedByEmployeeMovementCell = "REVIEWED_BY_EMPLOYEE_MOVEMENT_CELL";
	public const string ForwardedToHrBranchManager = "FORWARDED_TO_HR_BRANCH_MANAGER";
	public const string ForwardedToCeo = "FORWARDED_TO_CEO";
}
