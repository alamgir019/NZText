using NZ.HRM.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.HRM.Domain.Entities
{
    public class OfferLetter : BaseEntity
    {
        [Required]
        public string ApplicationTrackingId { get; set; } = string.Empty;
        [ForeignKey("OfferLetterApplicationTrackingId")]
        public ApplicationTracking? ApplicationTracking { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(100)]
        public string ReferenceNo { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string CandidateName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string FatherName { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Mobile { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Designation { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Post { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        public DateTime JoiningDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProbationPeriod { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Salary { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string JobStation { get; set; } = string.Empty;

        [MaxLength(20)]
        public string NotificationPeriod { get; set; } = string.Empty;

        [MaxLength(100)]
        public string SignatoryName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string CompanyGroup { get; set; } = string.Empty;
    }
}
