using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("mst_subunit", Schema = "master")]
    public class MstSubunit : BaseEntityWithSortOrder
    {
        public string UnitId { get; set; } = string.Empty;
        public string SubunitCode { get; set; } = string.Empty;
        public string SubunitName { get; set; } = string.Empty;

        // Navigation
        [ForeignKey("UnitId")]
        public MstUnit? Unit { get; set; }
    }
}
