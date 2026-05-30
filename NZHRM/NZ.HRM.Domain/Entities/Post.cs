using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class Post : BaseEntityWithSortOrder
    {
        public string PostName { get; set; } = string.Empty;
        public ICollection<Requisition>? Requisitions { get; set; }
    }
}
