using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("workflow_audit", Schema = "workflow")]
    public class WfWorkflowAudit : BaseEntityWithSortOrder
    {
        public string WorkflowTransactionId { get; set; } = string.Empty;
        public string? EventType { get; set; }
        public DateTime? EventDate { get; set; }
        public string? UserId { get; set; }
        public string? EventDetails { get; set; }

        [ForeignKey("WorkflowTransactionId")] public WfWorkflowTransaction? WorkflowTransaction { get; set; }
    }
}
