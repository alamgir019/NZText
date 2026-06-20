using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("pending_approval", Schema = "workflow")]
    public class WfPendingApproval : BaseEntityWithSortOrder
    {
        public string WorkflowTransactionId { get; set; } = string.Empty;
        public string ApproverId { get; set; } = string.Empty;
        public DateTime? PendingSince { get; set; }
        public string? PriorityLevel { get; set; }
        public DateOnly? DueDate { get; set; }

        [ForeignKey("WorkflowTransactionId")] public WfWorkflowTransaction? WorkflowTransaction { get; set; }
    }
}
