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
        public string GroupId { get; set; } = string.Empty;
        public string ComplexCode { get; set; } = string.Empty;
        public string ComplexName { get; set; } = string.Empty;

        // Navigation
        [ForeignKey("GroupId")]
        public MstGroup? Group { get; set; }
        public ICollection<MstUnit> Units { get; set; } = new HashSet<MstUnit>();
    }
}
