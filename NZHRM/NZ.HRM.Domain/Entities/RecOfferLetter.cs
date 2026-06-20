using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("offer_letter", Schema = "recruitment")]
    public class RecOfferLetter : BaseEntityWithSortOrder
    {
        public string CandidateId { get; set; } = string.Empty;
        public string? OfferDetails { get; set; }
        public DateTime? OfferDate { get; set; }

        [ForeignKey("CandidateId")] public RecCandidate? Candidate { get; set; }
    }
}
