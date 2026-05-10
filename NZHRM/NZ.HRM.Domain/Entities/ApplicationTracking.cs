using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class ApplicationTracking : BaseEntity
    {
        [Required]
        public string RequisitionId { get; set; } = string.Empty;
        [ForeignKey("ApplicationTrackingRequisitionId")]
        public Requisition? Requisition { get; set; }

        [Required]
        public string ApplicantName { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string FatherName { get; set; } = string.Empty;

        [Required]
        public string Mobile { get; set; } = string.Empty;

        public string? Qualification { get; set; }

        public string? CvPath { get; set; }

        public string? CirculationMedia { get; set; }
    }
}
