using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("inside_factory_status", Schema = "attendance")]
    public class AttInsideFactoryStatus : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public DateTime? LastPunchTime { get; set; }
        public string? CurrentStatus { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
