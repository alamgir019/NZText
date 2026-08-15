using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.Shared.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("attendance_lock", Schema = "attendance")]
    public class AttAttendanceLock : BaseEntityWithSortOrder
    {
        public string AttendanceMonth { get; set; } = string.Empty;
        public string? UnitId { get; set; }
        public DateTime? LockDate { get; set; }
        public string? LockedBy { get; set; }
        public DateTime? UnlockDate { get; set; }
        public string? UnlockedBy { get; set; }
        public string? Status { get; set; }

        [ForeignKey("UnitId")] public MstUnit? Unit { get; set; }
    }
}
