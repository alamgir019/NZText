using System.ComponentModel.DataAnnotations;

namespace NZ.Attendance.Application.AttendanceExceptions.Commands.DeleteAttendanceException
{
    public class DeleteAttendanceExceptionCommand
    {
        [Required]
        public string Id { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;
    }
}
