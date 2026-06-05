using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("recruitment_workflow", Schema = "recruitment")]
    public class RecRecruitmentWorkflow : BaseEntityWithSortOrder
    {
        public string CandidateId { get; set; } = string.Empty;
        public string? WorkflowId { get; set; }

        [ForeignKey("CandidateId")] public RecCandidate? Candidate { get; set; }
    }
}
