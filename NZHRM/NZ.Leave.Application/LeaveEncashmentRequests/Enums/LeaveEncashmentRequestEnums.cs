namespace NZ.Leave.Application.LeaveEncashmentRequests.Enums
{
    public static class LeaveEncashmentType
    {
        public const string CasualLeave = "CASUAL_LEAVE";
        public const string EarnedLeave = "EARNED_LEAVE";
        public const string MedicalLeave = "MEDICAL_LEAVE";
        public const string MaternityLeave = "MATERNITY_LEAVE";

        public static readonly string[] All = { CasualLeave, EarnedLeave, MedicalLeave, MaternityLeave };
    }

    public static class LeaveEncashmentRequestStatus
    {
        public const string Pending = "PENDING";
        public const string Forwarded = "FORWARDED";
        public const string Approved = "APPROVED";
        public const string Rejected = "REJECTED";
        public const string Cancelled = "CANCELLED";

        public static readonly string[] All = { Pending, Forwarded, Approved, Rejected, Cancelled };
    }

    public static class LeaveEncashmentRequestAction
    {
        public const string Forward = "FORWARD-TO-HR";
        public const string Reject = "REJECTED";

        public static readonly string[] All = { Forward, Reject };
    }
}
