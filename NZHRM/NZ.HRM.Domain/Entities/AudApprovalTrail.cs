using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("approval_trail", Schema = "audit")]
    public class AudApprovalTrail : BaseEntityWithSortOrder
    {
        public string WorkflowTransactionId { get; set; } = string.Empty;
        public string? ApproverId { get; set; }
        public string? ActionTaken { get; set; }
        public DateTime? ActionDate { get; set; }
        public string? Remarks { get; set; }

        [ForeignKey("WorkflowTransactionId")] public WfWorkflowTransaction? WorkflowTransaction { get; set; }
    }
}
