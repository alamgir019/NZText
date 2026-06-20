using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("candidate_documents", Schema = "recruitment")]
    public class RecCandidateDocument : BaseEntityWithSortOrder
    {
        public string CandidateId { get; set; } = string.Empty;
        public string? DocumentTypeId { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }

        [ForeignKey("CandidateId")] public RecCandidate? Candidate { get; set; }
    }
}
