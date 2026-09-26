using System.Text.Json.Serialization;

namespace NZ.Attendance.Application.AttendanceExceptions.Commands.ProcessAttendanceExceptionAction
{
    public class ProcessAttendanceExceptionActionCommand
    {
        public string RequestId { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string? Remarks { get; set; }

        [JsonIgnore]
        public string ProcessedBy { get; set; } = string.Empty;
    }
}