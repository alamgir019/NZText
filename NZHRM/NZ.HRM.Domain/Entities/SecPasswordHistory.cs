using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.HRM.Domain.Entities
{
    [Table("password_history", Schema = "security")]
    public class SecPasswordHistory : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime? ChangedDate { get; set; }

        [ForeignKey(nameof(UserId))] public SecUser? User { get; set; }
    }
}
