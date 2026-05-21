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
        public string? EnrollmentId { get; set; } = string.Empty;

        [MaxLength(100)]
        public string EmployeeNameEnglish { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? EmployeeNameBangla { get; set; }

        [Required]
        public string CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public Company? Company { get; set; }

        [Required]
        public string DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department? Department { get; set; }

        [Required]
        public string? SectionId { get; set; }
        [ForeignKey(nameof(SectionId))]
        public Section? Section { get; set; }

        public string? GradeId { get; set; }
        [ForeignKey(nameof(GradeId))]
        public Grade? Grade { get; set; }

        public string? CellId { get; set; }
        [ForeignKey(nameof(CellId))]
        public Cell? Cell { get; set; }

        public string? DesignationId { get; set; }
        [ForeignKey(nameof(DesignationId))]
        public Designation? Designation { get; set; }

        public EmployeeType? EmployeeType { get; set; }

        public Utility.Enum.Shift? Shift { get; set; }

        public EmployeeNature? EmployeeNature { get; set; }

        public Utility.Enum.Holiday? Holiday { get; set; }

        public decimal? ProposedMonthlySalary { get; set; }

        public DateTime? JoiningDate { get; set; }

        public DateTime? ConfirmationDate { get; set; }

        public EmployeeStatus? Status { get; set; } = EmployeeStatus.Draft;

        // Navigation property
        public EmployeePersonal? PersonalInfo { get; set; }
        public EmployeeVerification? VerificationInfo { get; set; }

    }
}
