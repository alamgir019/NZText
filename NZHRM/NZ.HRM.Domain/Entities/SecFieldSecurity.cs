using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.HRM.Domain.Entities
{
    [Table("field_security", Schema = "security")]
    public class SecFieldSecurity : BaseEntity
    {
        public string RoleId { get; set; } = string.Empty;
        public string ScreenCode { get; set; } = string.Empty;
        public string FieldName { get; set; } = string.Empty;
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }

        [ForeignKey(nameof(RoleId))] public SecRole? Role { get; set; }
    }
}
