namespace NZ.HRM.Domain.Enums
{
    /// <summary>
    /// Lifecycle of a learner permanency (confirmation) request.
    /// </summary>
    public enum LearnerConfirmationStatus
    {
        Forwarded = 1,
        Approved = 2, //ForwardedMovementCell
        Rejected = 3,
        ForwardedMovementSection = 4,
        ForwardedHR = 5,
        ForwardedCEO = 6,
    }
}
