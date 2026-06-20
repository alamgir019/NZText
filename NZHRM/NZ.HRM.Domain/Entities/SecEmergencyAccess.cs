using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("emergency_access", Schema = "security")]
    public class SecEmergencyAccess : BaseEntityWithSortOrder
    {
        public string UserId { get; set; } = string.Empty;
        public string GrantedBy { get; set; } = string.Empty;
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string? Reason { get; set; }
        public string? Status { get; set; }

        [ForeignKey("UserId")] public SecUser? User { get; set; }
    }
}
