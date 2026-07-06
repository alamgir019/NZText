using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("role", Schema = "security")]
    public class SecRole : BaseEntity
    {
        public string RoleCode { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<SecUserRole> UserRoles { get; set; } = new HashSet<SecUserRole>();
        public ICollection<SecRolePermission> RolePermissions { get; set; } = new HashSet<SecRolePermission>();
    }
}
