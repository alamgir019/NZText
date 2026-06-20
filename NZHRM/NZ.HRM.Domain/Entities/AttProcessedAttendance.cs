using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("processed_attendance", Schema = "attendance")]
    public class AttProcessedAttendance : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public DateOnly AttendanceDate { get; set; }
        public string? ShiftId { get; set; }
        public DateTime? ActualInTime { get; set; }
        public DateTime? ActualOutTime { get; set; }
        public DateTime? PayableInTime { get; set; }
        public DateTime? PayableOutTime { get; set; }
        public decimal WorkedHours { get; set; }
        public decimal OtWorkedHours { get; set; }
        public decimal OtPayableHours { get; set; }
        public string? AttendanceStatus { get; set; }
        public string? ProcessingStatus { get; set; }
        public DateTime? ProcessedDate { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
        [ForeignKey("ShiftId")] public MstShift? Shift { get; set; }
    }
}
