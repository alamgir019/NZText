using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("approval_history", Schema = "workflow")]
    public class WfApprovalHistory : BaseEntityWithSortOrder
    {
        public string WorkflowTransactionId { get; set; } = string.Empty;
        public int StepNo { get; set; }
        public string? ApproverId { get; set; }
        public string? ActionTaken { get; set; }
        public DateTime? ActionDate { get; set; }
        public string? Remarks { get; set; }

        [ForeignKey("WorkflowTransactionId")] public WfWorkflowTransaction? WorkflowTransaction { get; set; }
    }
}
