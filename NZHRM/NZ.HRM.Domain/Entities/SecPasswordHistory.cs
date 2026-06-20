using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("password_history", Schema = "security")]
    public class SecPasswordHistory : BaseEntityWithSortOrder
    {
        public string UserId { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime? ChangedDate { get; set; }

        [ForeignKey("UserId")] public SecUser? User { get; set; }
    }
}
