namespace NZ.Leave.Domain.Constants
{
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
        public const string ForwardedToSectionInCharge = "FORWARDED-TO-SECTION-IN-CHARGE";
        public const string ForwardedToAttendanceCell = "FORWARDED-TO-ATTENDANCE-CELL";
        public const string ForwardedToHR = "FORWARDED-TO-HR";
        public const string RejectedFromHR = "REJECTED-FROM-HR";
        public const string ForwardedToCEO = "FORWARDED-TO-CEO";
        public const string RejectedFromCEO = "REJECTED-FROM-CEO";

        public static readonly string[] All = { ForwardedToSectionInCharge, ForwardedToAttendanceCell, ForwardedToHR, RejectedFromHR, ForwardedToCEO, RejectedFromCEO };
    }

    public static class LeaveEncashmentType
    {
        public const string CasualLeave = "CASUAL_LEAVE";
        public const string EarnedLeave = "EARNED_LEAVE";
        public const string MedicalLeave = "MEDICAL_LEAVE";
        public const string MaternityLeave = "MATERNITY_LEAVE";

        public static readonly string[] All = { CasualLeave, EarnedLeave, MedicalLeave, MaternityLeave };
    }
}
