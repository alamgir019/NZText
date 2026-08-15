using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;
using NZ.Shared.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("thana", Schema = "lookup")]
    public class LookThana : BaseEntityWithSortOrder
    {
        public string ThanaName { get; set; } = string.Empty;
        public string? ThanaNameBangla { get; set; }

        // Foreign key to District
        public string DistrictId { get; set; } = string.Empty;

        [ForeignKey(nameof(DistrictId))]
        public LookDistrict? District { get; set; }
    }
}
