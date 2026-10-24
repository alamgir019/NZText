namespace NZ.HRM.Domain.Constants;

public static class PayIncrementStatuses
{
	public const string Pending = "Pending"; // ForwardedToDirector
    public const string ForwardedToDirector = "ForwardedToDirector";
	public const string Approved = "Approved"; // ForwardedToMovementCell
    public const string Rejected = "Rejected"; // ForwardedToMovementCell
    public const string ForwardedToMovementSection = "ForwardedToMovementSection";
	public const string ForwardedToHR = "ForwardedToHR";
	public const string ForwardedToCEO = "ForwardedToCEO";
}
