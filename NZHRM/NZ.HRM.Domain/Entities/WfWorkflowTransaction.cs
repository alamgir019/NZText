using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("workflow_transaction", Schema = "workflow")]
    public class WfWorkflowTransaction : BaseEntityWithSortOrder
    {
        public string WorkflowMasterId { get; set; } = string.Empty;
        public string? ReferenceTable { get; set; }
        public string? ReferenceId { get; set; }
        public string? RequestorId { get; set; }
        public DateTime? RequestDate { get; set; }
        public int CurrentStepNo { get; set; }
        public string? CurrentApproverId { get; set; }
        public string? WorkflowStatus { get; set; }
        public DateTime? CompletionDate { get; set; }

        [ForeignKey("WorkflowMasterId")] public WfWorkflowMaster? WorkflowMaster { get; set; }

        public ICollection<WfApprovalHistory> ApprovalHistory { get; set; } = new HashSet<WfApprovalHistory>();
        public ICollection<WfPendingApproval> PendingApprovals { get; set; } = new HashSet<WfPendingApproval>();
        public ICollection<WfNotificationQueue> Notifications { get; set; } = new HashSet<WfNotificationQueue>();
        public ICollection<WfWorkflowAttachment> Attachments { get; set; } = new HashSet<WfWorkflowAttachment>();
        public ICollection<WfWorkflowAudit> Audits { get; set; } = new HashSet<WfWorkflowAudit>();
    }
}
