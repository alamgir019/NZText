using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("user_session", Schema = "security")]
    public class SecUserSession : BaseEntityWithSortOrder
    {
        public string UserId { get; set; } = string.Empty;
        public DateTime? LoginDateTime { get; set; }
        public DateTime? LogoutDateTime { get; set; }
        public string? IPAddress { get; set; }
        public string? DeviceInfo { get; set; }
        public string? SessionStatus { get; set; }

        [ForeignKey("UserId")] public SecUser? User { get; set; }
    }
}
