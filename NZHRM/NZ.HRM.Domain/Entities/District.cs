using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class District : BaseEntity
    {
        public string DistrictName { get; set; } = string.Empty;

        public ICollection<Location>? Locations { get; set; }
    }
}
