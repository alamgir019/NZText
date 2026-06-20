using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("workflow_step", Schema = "workflow")]
    public class WfWorkflowStep : BaseEntityWithSortOrder
    {
        public string WorkflowMasterId { get; set; } = string.Empty;
        public int StepNo { get; set; }
        public string? StepName { get; set; }
        public string? RoleId { get; set; }
        public bool MandatoryFlag { get; set; }
        public bool ActiveFlag { get; set; }

        [ForeignKey("WorkflowMasterId")] public WfWorkflowMaster? WorkflowMaster { get; set; }
    }
}
