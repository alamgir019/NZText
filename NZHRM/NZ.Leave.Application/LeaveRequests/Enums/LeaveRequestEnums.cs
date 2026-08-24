namespace NZ.Leave.Application.LeaveRequests.Enums
{
    public static class LeaveType
    {
        public const string CasualLeave = "CASUAL_LEAVE";
        public const string EarnedLeave = "EARNED_LEAVE";
        public const string MedicalLeave = "MEDICAL_LEAVE";

        public static readonly string[] All = { CasualLeave, EarnedLeave, MedicalLeave };
    }

    public static class RequestStatus
    {
        public const string Draft = "DRAFT";
        public const string Forwarded = "FORWARDED";
        public const string Rejected = "REJECTED";
        public const string Cancelled = "CANCELLED";

        public static readonly string[] All = { Draft, Forwarded, Rejected, Cancelled };
    }
}
