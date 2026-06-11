using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("mst_group_complex", Schema = "master")]
    public class MstGroupComplex : BaseEntityWithSortOrder
    {
        public string GroupCode { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;

        // Navigation
        public ICollection<MstUnit> Units { get; set; } = new HashSet<MstUnit>();
    }
}
