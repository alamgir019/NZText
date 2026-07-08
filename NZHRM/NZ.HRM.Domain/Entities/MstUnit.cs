using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("mst_unit", Schema = "master")]
    public class MstUnit : BaseEntityWithSortOrder
    {
        public string ComplexId { get; set; } = string.Empty;
        public string UnitCode { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
        public bool IsCompliant { get; set; }

        // Navigation
        [ForeignKey("ComplexId")]
        public MstGroupComplex? Complex { get; set; }
        public ICollection<MstSubunit> Subunits { get; set; } = new HashSet<MstSubunit>();
    }
}
