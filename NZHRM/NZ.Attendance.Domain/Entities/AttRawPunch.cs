using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Domain.Entities
{
    [Table("raw_punch", Schema = "attendance")]
    public class AttRawPunch : BaseEntity
    {
        public string? EmployeeId { get; set; }
        public string? EmployeeCode { get; set; }
        public DateOnly PunchDate { get; set; }
        public TimeOnly PunchTime { get; set; }
        public string? DeviceId { get; set; }
        public string? DeviceLocation { get; set; }
        public string? VerificationMode { get; set; }
        public string? PunchSource { get; set; }
        public string? ImportBatchId { get; set; }
        public string? PunchStatus { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
        [ForeignKey("DeviceId")] public AttDeviceMaster? Device { get; set; }
        public string? PunchType { get; set; }
    }
}
