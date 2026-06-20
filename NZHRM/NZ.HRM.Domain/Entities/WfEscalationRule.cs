using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("escalation_rule", Schema = "workflow")]
    public class WfEscalationRule : BaseEntityWithSortOrder
    {
        public string WorkflowMasterId { get; set; } = string.Empty;
        public int StepNo { get; set; }
        public int EscalateAfterHours { get; set; }
        public string? EscalateToRoleId { get; set; }
        public bool ActiveFlag { get; set; }

        [ForeignKey("WorkflowMasterId")] public WfWorkflowMaster? WorkflowMaster { get; set; }
    }
}
