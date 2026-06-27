using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("thana", Schema = "lookup")]
    public class LookThana : BaseEntityWithSortOrder
    {
        public string ThanaName { get; set; } = string.Empty;

        // Foreign key to District
        public string DistrictId { get; set; } = string.Empty;

        [ForeignKey(nameof(DistrictId))]
        public LookDistrict? District { get; set; }
    }
}
