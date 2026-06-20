using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("permission", Schema = "security")]
    public class SecPermission : BaseEntityWithSortOrder
    {
        public string PermissionCode { get; set; } = string.Empty;
        public string PermissionName { get; set; } = string.Empty;
        public string? ModuleName { get; set; }
        public string? PermissionType { get; set; }
        public bool ActiveFlag { get; set; }
    }
}
