using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("interview_schedule", Schema = "recruitment")]
    public class RecInterviewSchedule : BaseEntityWithSortOrder
    {
        public string CandidateId { get; set; } = string.Empty;
        public DateTime? InterviewDate { get; set; }
        public string? InterviewPanelId { get; set; }
        public string? InterviewerId { get; set; }
        public string? InterviewType { get; set; }

        [ForeignKey("CandidateId")] public RecCandidate? Candidate { get; set; }
    }
}
