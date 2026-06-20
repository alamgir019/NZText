using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("system_event", Schema = "audit")]
    public class AudSystemEvent : BaseEntityWithSortOrder
    {
        public string? EventType { get; set; }
        public DateTime? EventDateTime { get; set; }
        public string? UserId { get; set; }
        public string? EventDescription { get; set; }

        [ForeignKey("UserId")] public SecUser? User { get; set; }
    }
}
