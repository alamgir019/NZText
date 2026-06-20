using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("payroll_processing_group", Schema = "master")]
    public class MstPayrollProcessingGroup : BaseEntityWithSortOrder
    {
        [MaxLength(20)]
        public string ProcessingGroupCode { get; set; } = string.Empty;

        [MaxLength(100)]
        public string ProcessingGroupName { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

        public bool ActiveFlag { get; set; }

        public ICollection<HrmEmployeeMaster> Employees { get; set; } = new HashSet<HrmEmployeeMaster>();
    }
}
