using System.ComponentModel.DataAnnotations;

namespace NZ.Attendance.Application.AttendanceExceptions.Commands.ReviewAttendanceException
{
    public class SubmitAttendanceExceptionCommand
    {
        [Required] public string Id { get; set; } = string.Empty;
        [Required] public string UserId { get; set; } = string.Empty;
        [MaxLength(500)] public string? Comments { get; set; }
    }

    public class ApproveAttendanceExceptionCommand
    {
        [Required] public string Id { get; set; } = string.Empty;
        [Required] public string UserId { get; set; } = string.Empty;
        [MaxLength(500)] public string? Comments { get; set; }
    }

    public class RejectAttendanceExceptionCommand
    {
        [Required] public string Id { get; set; } = string.Empty;
        [Required] public string UserId { get; set; } = string.Empty;
        [Required][MaxLength(500)] public string Comments { get; set; } = string.Empty;
    }

    public class CancelAttendanceExceptionCommand
    {
        [Required] public string Id { get; set; } = string.Empty;
        [Required] public string UserId { get; set; } = string.Empty;
        [MaxLength(500)] public string? Comments { get; set; }
    }
}
