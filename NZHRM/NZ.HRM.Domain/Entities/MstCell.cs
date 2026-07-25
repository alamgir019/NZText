using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("mst_cell", Schema = "master")]
    public class MstCell : BaseEntityWithSortOrder
    {
        public string SectionId { get; set; } = string.Empty;
        public string CellCode { get; set; } = string.Empty;
        public string CellName { get; set; } = string.Empty;

        // Navigation
        [ForeignKey("SectionId")]
        public MstSection? Section { get; set; }
        public string? NameBangla { get; set; }
    }
}
