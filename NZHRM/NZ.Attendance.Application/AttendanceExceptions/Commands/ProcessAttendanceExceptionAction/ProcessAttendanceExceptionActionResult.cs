namespace NZ.Attendance.Application.AttendanceExceptions.Commands.ProcessAttendanceExceptionAction
{
    public class ProcessAttendanceExceptionActionResult
    {
        public bool Success { get; set; }
        public string? ErrorCode { get; set; }
        public string RequestId { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}