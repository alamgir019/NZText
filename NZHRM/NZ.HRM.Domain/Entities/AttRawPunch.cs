using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("raw_punch", Schema = "attendance")]
    public class AttRawPunch : BaseEntityWithSortOrder
    {
        public string? EmployeeId { get; set; }
        public string? CardNo { get; set; }
        public DateTime PunchDateTime { get; set; }
        public DateOnly PunchDate { get; set; }
        public TimeSpan PunchTime { get; set; }
        public string? DeviceId { get; set; }
        public string? DeviceLocation { get; set; }
        public string? VerificationMode { get; set; }
        public string? PunchSource { get; set; }
        public string? ImportBatchId { get; set; }
        public string? PunchStatus { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("DeviceId")] public AttDeviceMaster? Device { get; set; }
    }
}
