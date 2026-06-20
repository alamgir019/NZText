using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("delegation", Schema = "workflow")]
    public class WfDelegation : BaseEntityWithSortOrder
    {
        public string FromUserId { get; set; } = string.Empty;
        public string ToUserId { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? WorkflowMasterId { get; set; }
        public bool ActiveFlag { get; set; }
    }
}
