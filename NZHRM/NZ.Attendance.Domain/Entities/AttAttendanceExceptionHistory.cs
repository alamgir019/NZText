using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.Attendance.Domain.Enums;
using NZ.Shared.Domain.Common;

namespace NZ.Attendance.Domain.Entities
{
    [Table("attendance_exception_history", Schema = "attendance")]
    public class AttAttendanceExceptionHistory : BaseEntityWithSortOrder
    {
        public string AttendanceExceptionId { get; set; } = string.Empty;
        public AttendanceExceptionStatus FromStatus { get; set; }
        public AttendanceExceptionStatus ToStatus { get; set; }
        public string ActionBy { get; set; } = string.Empty;
        public DateTime ActionOn { get; set; } = DateTime.UtcNow;
        public string? Comments { get; set; }

        [ForeignKey("AttendanceExceptionId")]
        public AttAttendanceException? AttendanceException { get; set; }
    }
}
