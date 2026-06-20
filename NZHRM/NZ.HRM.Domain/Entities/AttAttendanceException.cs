using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("attendance_exception", Schema = "attendance")]
    public class AttAttendanceException : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public DateOnly AttendanceDate { get; set; }
        public string? ExceptionType { get; set; }
        public string? Severity { get; set; }
        public string? Remarks { get; set; }
        public bool ResolvedFlag { get; set; }
        public string? ResolvedBy { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
