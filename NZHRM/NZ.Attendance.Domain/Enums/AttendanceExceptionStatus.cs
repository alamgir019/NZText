namespace NZ.Attendance.Domain.Enums
{
    public enum AttendanceExceptionStatus
    {
        Pending = 0,
        Submitted = 1,
        Forwarded = 2,
        Approved = 3,
        Rejected = 4,
        ForwardedToHR = 5,
        ForwardedToIT = 6,
        Cancelled = 7
    }
}
