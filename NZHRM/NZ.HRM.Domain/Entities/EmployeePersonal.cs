using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Domain.Entities
{
    public class EmployeePersonal : BaseEntity
    {
        [ForeignKey(nameof(Employee))]
        public string EmployeeId { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string EmployeeCode { get; set; } = string.Empty;

        // Personal Information
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public MaritalStatus MaritalStatus { get; set; }

        [Required]
        [MaxLength(20)]
        public string MobileNumber { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? EmailAddress { get; set; }

        [Required]
        public DocumentType DocumentType { get; set; }

        [Required]
        [MaxLength(50)]
        public string DocumentNumber { get; set; } = string.Empty;

        public BloodGroup? BloodGroup { get; set; }

        [Required]
        public Religion Religion { get; set; }

        [Required]
        public Nationality Nationality { get; set; }

        // Additional Information
        [Required]
        [MaxLength(100)]
        public string FatherNameEnglish { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? FatherNameBangla { get; set; }

        [Required]
        [MaxLength(100)]
        public string MotherNameEnglish { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? MotherNameBangla { get; set; }

        [MaxLength(100)]
        public string? SpouseName { get; set; }

        [MaxLength(20)]
        public string? SpouseMobile { get; set; }

        [MaxLength(50)]
        public string? TinNumber { get; set; } // 12-digit TIN (Optional)

        [MaxLength(100)]
        public string? EmployeeReference { get; set; }

        [MaxLength(50)]
        public string? ReferencePersonId { get; set; }

        [MaxLength(200)]
        public string? PermanentVillageAreaRoad { get; set; }

        [MaxLength(100)]
        public string? PermanentPostOffice { get; set; }

        [MaxLength(100)]
        public string? PermanentThana { get; set; }

        [MaxLength(100)]
        public string? PermanentDistrict { get; set; }

        [MaxLength(100)]
        public string? PermanentDivision { get; set; }

        [MaxLength(200)]
        public string? PresentVillageAreaRoad { get; set; }

        [MaxLength(100)]
        public string? PresentPostOffice { get; set; }

        [MaxLength(100)]
        public string? PresentThana { get; set; }

        [MaxLength(100)]
        public string? PresentDistrict { get; set; }

        [MaxLength(100)]
        public string? PresentDivision { get; set; }

        // Navigation property
        public EmployeeMaster? Employee { get; set; }
    }
}
