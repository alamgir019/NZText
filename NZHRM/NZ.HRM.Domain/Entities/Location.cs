using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class Location : BaseEntityWithSortOrder
    {
        public string LocationName { get; set; } = string.Empty;
        public string DistrictId { get; set; } = string.Empty;
        public District? District { get; set; }
        // Relationship through mapping entity
        public ICollection<CompanyLocation>? CompanyLocations { get; set; }
    }
}
