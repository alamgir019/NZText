using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.Shared.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("device_master", Schema = "attendance")]
    public class AttDeviceMaster : BaseEntityWithSortOrder
    {
        public string DeviceCode { get; set; } = string.Empty;
        public string DeviceName { get; set; } = string.Empty;
        public string? IPAddress { get; set; }
        public string? Location { get; set; }
        public string? UnitId { get; set; }
        public bool Status { get; set; }
        public DateTime? LastSyncTime { get; set; }

        [ForeignKey("UnitId")] public MstUnit? Unit { get; set; }
    }
}
