using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public int Order { get; set; }
    }
}
