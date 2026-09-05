using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NZ.Attendance.Application.AttendanceExceptions.Commands.CreateAttendanceExceptions
{
    public class CreateAttendanceExceptionItem
    {
        [Required]
        public string EmployeeId { get; set; } = string.Empty;

        [Required]
        public DateOnly AttendanceDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ExceptionType { get; set; }

        [MaxLength(20)]
        public string? Severity { get; set; }

        [MaxLength(500)]
        public string? Remarks { get; set; }
    }

    /// <summary>Creates a batch of attendance exceptions in one transaction.</summary>
    public class CreateAttendanceExceptionsCommand
    {
        [Required]
        public List<CreateAttendanceExceptionItem> Items { get; set; } = new();

        /// <summary>When true, each created row is immediately forwarded to the attendance cell.</summary>
        public bool SubmitImmediately { get; set; }

        /// <summary>User performing the action.</summary>
        [Required]
        public string UserId { get; set; } = string.Empty;
    }
}
