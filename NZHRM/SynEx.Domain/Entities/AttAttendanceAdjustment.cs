using NZ.HRM.Domain.Entities;
using NZ.Shared.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.Attendance.Domain.Entities
{
    [Table("attendance_adjustment", Schema = "attendance")]
    public class AttAttendanceAdjustment : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public DateOnly AttendanceDate { get; set; }
        public string? AdjustmentType { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public string? Reason { get; set; }
        public string? RequestedBy { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? Status { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
