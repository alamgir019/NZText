using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Domain.Entities
{
    public class Requisition : BaseEntity
    {
        [Required]
        public string PostId { get; set; } = string.Empty;
        [ForeignKey(nameof(PostId))]
        public Post? Post { get; set; }

        public string DesignationId { get; set; } = string.Empty;
        [ForeignKey(nameof(DesignationId))]
        public Designation? Designation { get; set; }

        public string CompanyId { get; set; } = string.Empty;
        [ForeignKey(nameof(CompanyId))]
        public Company? Company { get; set; }

        [Required]
        public string SalaryRange { get; set; } = string.Empty;

        public string TermsAndCondition { get; set; } = string.Empty;

        [Required]
        public JobType JobType { get; set; } = JobType.Permanent;

        [Required]
        public DateTime RequisitionDate { get; set; }

        public string? CirculationMedia { get; set; }
    }
}
