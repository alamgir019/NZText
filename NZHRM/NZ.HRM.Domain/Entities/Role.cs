using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class Role : BaseEntityWithSortOrder
    {
        public string RoleName { get; set; } = string.Empty;
    }
}
