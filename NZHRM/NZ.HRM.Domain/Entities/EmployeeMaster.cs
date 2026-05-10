using NZ.HRM.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Domain.Entities
{
    public class EmployeeMaster : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string EmployeeCode { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string EnrollmentId { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string EmployeeNameEnglish { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? EmployeeNameBangla { get; set; }

        [Required]
        public string CompanyId { get; set; } = string.Empty;
        [ForeignKey(nameof(CompanyId))]
        public Company? Company { get; set; }

        [Required]
        public string DepartmentId { get; set; } = string.Empty;
        [ForeignKey(nameof(DepartmentId))]
        public Department? Department { get; set; }

        [Required]
        public string SectionId { get; set; } = string.Empty;
        [ForeignKey(nameof(SectionId))]
        public Section? Section { get; set; }

        [Required]
        public string GradeId { get; set; } = string.Empty;
        [ForeignKey(nameof(GradeId))]
        public Grade? Grade { get; set; }

        [Required]
        public EmployeeType EmployeeType { get; set; }

        [Required]
        public Utility.Enum.Shift Shift { get; set; }

        [Required]
        public EmployeeNature EmployeeNature { get; set; }

        public Utility.Enum.Holiday Holiday { get; set; }

        public decimal? ProposedMonthlySalary { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        public DateTime? ConfirmationDate { get; set; }

        [Required]
        public EmployeeStatus Status { get; set; } = EmployeeStatus.Draft;

        // Navigation property
        public EmployeePersonal? PersonalInfo { get; set; }
        public EmployeeVerification? VerificationInfo { get; set; }
    }
}
