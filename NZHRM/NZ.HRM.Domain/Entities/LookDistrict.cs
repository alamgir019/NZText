using NZ.HRM.Domain.Common;
using NZ.Shared.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.HRM.Domain.Entities
{
    [Table("district", Schema = "lookup")]
    public class LookDistrict : BaseEntityWithSortOrder
    {
        public string DistrictName { get; set; } = string.Empty;
        public string? DistrictNameBangla { get; set; }

        // Foreign key to Division
        public string DivisionId { get; set; } = string.Empty;

        [ForeignKey(nameof(DivisionId))]
        public LookDivision? Division { get; set; }

        // Navigation properties
        public ICollection<LookThana>? Thanas { get; set; }
        public ICollection<MstSubunit>? MstSubunits { get; set; }
    }
}
