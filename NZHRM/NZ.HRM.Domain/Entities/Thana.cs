using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class Thana : BaseEntity
    {
        public string ThanaName { get; set; } = string.Empty;

        // Foreign key to District
        public string DistrictId { get; set; } = string.Empty;

        [ForeignKey(nameof(DistrictId))]
        public District? District { get; set; }
    }
}
