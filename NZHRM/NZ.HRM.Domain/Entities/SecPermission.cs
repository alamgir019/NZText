using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Domain.Entities
{
    [Table("permission", Schema = "security")]
    public class SecPermission : BaseEntity
    {
        public string PermissionCode { get; set; } = string.Empty;
        public string PermissionName { get; set; } = string.Empty;
        public string? ModuleName { get; set; }
        public PermissionType? PermissionType { get; set; }
    }
}
