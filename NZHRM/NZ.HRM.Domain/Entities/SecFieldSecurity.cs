using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("field_security", Schema = "security")]
    public class SecFieldSecurity : BaseEntityWithSortOrder
    {
        public string RoleId { get; set; } = string.Empty;
        public string ScreenCode { get; set; } = string.Empty;
        public string FieldName { get; set; } = string.Empty;
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }

        [ForeignKey("RoleId")] public SecRole? Role { get; set; }
    }
}
