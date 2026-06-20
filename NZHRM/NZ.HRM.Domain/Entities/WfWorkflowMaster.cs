using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("workflow_master", Schema = "workflow")]
    public class WfWorkflowMaster : BaseEntityWithSortOrder
    {
        public string WorkflowCode { get; set; } = string.Empty;
        public string WorkflowName { get; set; } = string.Empty;
        public string? ModuleName { get; set; }
        public string? Description { get; set; }
        public bool ActiveFlag { get; set; }

        public ICollection<WfWorkflowStep> Steps { get; set; } = new HashSet<WfWorkflowStep>();
    }
}
