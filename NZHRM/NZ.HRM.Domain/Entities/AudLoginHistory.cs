using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("login_history", Schema = "audit")]
    public class AudLoginHistory : BaseEntityWithSortOrder
    {
        public string UserId { get; set; } = string.Empty;
        public DateTime? LoginDateTime { get; set; }
        public DateTime? LogoutDateTime { get; set; }
        public string? IPAddress { get; set; }
        public string? LoginStatus { get; set; }

        [ForeignKey("UserId")] public SecUser? User { get; set; }
    }
}
