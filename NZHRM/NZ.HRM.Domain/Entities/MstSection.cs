using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("mst_section", Schema = "master")]
    public class MstSection : BaseEntityWithSortOrder
    {
        public string SectionCode { get; set; } = string.Empty;
        public string SectionName { get; set; } = string.Empty;
        public string? SectionNameBangla { get; set; }
        public string DepartmentId { get; set; } = string.Empty;

        // Navigation
        [ForeignKey("SectionId")]
        public MstDepartment? Department { get; set; } = new MstDepartment();
        public ICollection<MstCell> Cells { get; set; } = new HashSet<MstCell>();
    }
}
