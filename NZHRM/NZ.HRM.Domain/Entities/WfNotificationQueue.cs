using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("notification_queue", Schema = "workflow")]
    public class WfNotificationQueue : BaseEntityWithSortOrder
    {
        public string WorkflowTransactionId { get; set; } = string.Empty;
        public string RecipientId { get; set; } = string.Empty;
        public string? NotificationType { get; set; }
        public DateTime? NotificationDate { get; set; }
        public string? DeliveryStatus { get; set; }

        [ForeignKey("WorkflowTransactionId")] public WfWorkflowTransaction? WorkflowTransaction { get; set; }
    }
}
