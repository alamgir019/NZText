using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("role_permission", Schema = "security")]
    public class SecRolePermission : BaseEntityWithSortOrder
    {
        public string RoleId { get; set; } = string.Empty;
        public string PermissionId { get; set; } = string.Empty;
        public bool ActiveFlag { get; set; }

        [ForeignKey("RoleId")] public SecRole? Role { get; set; }
        [ForeignKey("PermissionId")] public SecPermission? Permission { get; set; }
    }
}
