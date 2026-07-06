using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("user_role", Schema = "security")]
    public class SecUserRole : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public string RoleId { get; set; } = string.Empty;
        public DateOnly? EffectiveDate { get; set; }
        public DateOnly? ExpiryDate { get; set; }

        [ForeignKey(nameof(UserId))] public SecUser? User { get; set; }
        [ForeignKey(nameof(RoleId))] public SecRole? Role { get; set; }
    }
}
