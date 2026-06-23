using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("mst_unit", Schema = "master")]
    public class MstUnit : BaseEntityWithSortOrder
    {
        public string GroupId { get; set; } = string.Empty;
        public string UnitCode { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
        public bool IsCompliant { get; set; }

        // Navigation
        [ForeignKey("GroupId")]
        public MstGroup? Group { get; set; }
        public ICollection<MstSubunit> Subunits { get; set; } = new HashSet<MstSubunit>();
    }
}
