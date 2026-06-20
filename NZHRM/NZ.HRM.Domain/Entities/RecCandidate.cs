using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("candidate", Schema = "recruitment")]
    public class RecCandidate : BaseEntityWithSortOrder
    {
        public string CandidateCode { get; set; } = string.Empty;
        public string CandidateName { get; set; } = string.Empty;
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? GenderId { get; set; }
        public decimal? ExpectedSalary { get; set; }
        public decimal? CurrentSalary { get; set; }
        public string? Source { get; set; }
        public string? Status { get; set; }

        [ForeignKey("PositionId")] public RecJobPosition? JobPosition { get; set; }
    }
}
