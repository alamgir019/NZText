using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class Holiday : BaseEntity
    {
        public string HolidayName { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public string? Description { get; set; }
    }
}
