using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.HRM.Domain.Entities
{
    [Table("role_permission", Schema = "security")]
    public class SecRolePermission : BaseEntity
    {
        public string RoleId { get; set; } = string.Empty;
        public string PermissionId { get; set; } = string.Empty;

        [ForeignKey(nameof(RoleId))] public SecRole? Role { get; set; }
        [ForeignKey(nameof(PermissionId))] public SecPermission? Permission { get; set; }
    }
}
