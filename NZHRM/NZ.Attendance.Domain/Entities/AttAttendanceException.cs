using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.Attendance.Domain.Enums;
using NZ.HRM.Domain.Entities;
using NZ.Shared.Domain.Common;

namespace NZ.Attendance.Domain.Entities
{
    [Table("attendance_exception", Schema = "attendance")]
    public class AttAttendanceException : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public DateOnly AttendanceDate { get; set; }
        public string? ExceptionType { get; set; }
        public string? Severity { get; set; }
        public string? Remarks { get; set; }

        // Current workflow state. Changed only through AttendanceExceptionWorkflow.
        // Who forwarded/approved and when is recorded in History.
        public AttendanceExceptionStatus Status { get; set; } = AttendanceExceptionStatus.Draft;

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }

        public ICollection<AttAttendanceExceptionHistory> History { get; set; }
            = new List<AttAttendanceExceptionHistory>();
    }
}
