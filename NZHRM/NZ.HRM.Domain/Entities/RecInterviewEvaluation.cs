using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("interview_evaluation", Schema = "recruitment")]
    public class RecInterviewEvaluation : BaseEntityWithSortOrder
    {
        public string InterviewScheduleId { get; set; } = string.Empty;
        public string? EvaluatorId { get; set; }
        public string? Evaluation { get; set; }
        public string? Remarks { get; set; }

        [ForeignKey("InterviewScheduleId")] public RecInterviewSchedule? InterviewSchedule { get; set; }
    }
}
