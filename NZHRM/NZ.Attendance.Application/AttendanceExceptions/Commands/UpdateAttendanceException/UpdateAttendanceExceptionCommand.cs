using System;
using System.ComponentModel.DataAnnotations;

namespace NZ.Attendance.Application.AttendanceExceptions.Commands.UpdateAttendanceException
{
    public class UpdateAttendanceExceptionCommand
    {
        [Required]
        public string Id { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string? ExceptionType { get; set; }

        [MaxLength(20)]
        public string? Severity { get; set; }

        [MaxLength(500)]
        public string? Remarks { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;
    }
}
