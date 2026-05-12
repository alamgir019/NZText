using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class Division : BaseEntity
    {
        public string DivisionName { get; set; } = string.Empty;

        // Navigation property
        public ICollection<District>? Districts { get; set; }
    }
}
