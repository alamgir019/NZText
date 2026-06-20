using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("mst_group", Schema = "master")]
    public class MstGroup : BaseEntityWithSortOrder
    {
        public string GroupCode { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;

        // Navigation
        public ICollection<MstGroupComplex> MstGroupComplexes { get; set; } = new HashSet<MstGroupComplex>();
    }
}
