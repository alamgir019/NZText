using System;
using NZ.Attendance.Domain.Enums;

namespace NZ.Attendance.Application.AttendanceExceptions.Dto
{
    public class AttendanceExceptionDto
    {
        public string Id { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public string? EmployeeCode { get; set; }
        public string? EmployeeName { get; set; }
        public DateOnly AttendanceDate { get; set; }
        public string? ExceptionType { get; set; }
        public string? Severity { get; set; }
        public string? Remarks { get; set; }
        public AttendanceExceptionStatus Status { get; set; }

        // Projected from the history trail, not stored on the entity.
        public string? ForwardedBy { get; set; }
        public DateTime? ForwardedOn { get; set; }
        public string? ReviewedBy { get; set; }
        public DateTime? ReviewedOn { get; set; }
        public string? ReviewRemarks { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
