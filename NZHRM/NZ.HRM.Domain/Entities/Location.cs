using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class Location : BaseEntity
    {
        public string LocationName { get; set; } = string.Empty;
        public string DistrictId { get; set; } = string.Empty;
        public District? District { get; set; }
        public ICollection<Company>? Companies { get; set; }
    }
}
