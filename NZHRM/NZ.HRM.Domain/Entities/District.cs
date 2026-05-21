using NZ.HRM.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.HRM.Domain.Entities
{
    public class District : BaseEntityWithSortOrder
    {
        public string DistrictName { get; set; } = string.Empty;

        // Foreign key to Division
        public string DivisionId { get; set; } = string.Empty;

        [ForeignKey(nameof(DivisionId))]
        public Division? Division { get; set; }

        // Navigation properties
        public ICollection<Thana>? Thanas { get; set; }
        public ICollection<Location>? Locations { get; set; }
    }
}
