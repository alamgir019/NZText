using NZ.HRM.Domain.Common;
using NZ.HRM.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.Attendance.Domain.Entities
{
    [Table("device_sync_log", Schema = "attendance")]
    public class AttDeviceSyncLog : BaseEntity
    {
        public string DeviceId { get; set; } = string.Empty;
        public DateTime? SyncStartTime { get; set; }
        public DateTime? SyncEndTime { get; set; }
        public int PunchCount { get; set; }
        public string? SyncStatus { get; set; }
        public string? ErrorMessage { get; set; }

        [ForeignKey("DeviceId")] public AttDeviceMaster? Device { get; set; }
    }
}
