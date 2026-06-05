using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("mst_section", Schema = "master")]
    public class MstSection : BaseEntityWithSortOrder
    {
        public string SectionCode { get; set; } = string.Empty;
        public string SectionName { get; set; } = string.Empty;

        // Navigation
        public ICollection<MstDepartmentSection> DepartmentSections { get; set; } = new HashSet<MstDepartmentSection>();
        public ICollection<MstCell> Cells { get; set; } = new HashSet<MstCell>();
    }
}
