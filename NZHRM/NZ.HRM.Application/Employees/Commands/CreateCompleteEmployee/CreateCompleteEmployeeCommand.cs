using System.ComponentModel.DataAnnotations;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Employees.Commands.CreateCompleteEmployee;

public class CreateCompleteEmployeeCommand
{
    // Basic Employment Information
    [Required(ErrorMessage = "Employee code is required")]
    [MaxLength(50, ErrorMessage = "Employee code must not exceed 50 characters")]
    public string EmployeeCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Employee name (English) is required")]
    [MaxLength(100, ErrorMessage = "Employee name must not exceed 100 characters")]
    public string EmployeeNameEnglish { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "Employee name (Bangla) must not exceed 100 characters")]
    public string? EmployeeNameBangla { get; set; }

    // Company & Organization
    [Required(ErrorMessage = "Company ID is required")]
    public string CompanyId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department ID is required")]
    public string DepartmentId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Section ID is required")]
    public string SectionId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Grade ID is required")]
    public string GradeId { get; set; } = string.Empty;

    // Employment Details
    [Required(ErrorMessage = "Employee type is required")]
    public EmployeeType EmployeeType { get; set; }

    [Required(ErrorMessage = "Shift ID is required")]
    public string ShiftId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Employee nature is required")]
    public EmployeeNature EmployeeNature { get; set; }

    public string? HolidayId { get; set; }

    [Required(ErrorMessage = "Joining date is required")]
    public DateTime JoiningDate { get; set; }

    public DateTime? ConfirmationDate { get; set; }

    // Personal Information
    [Required(ErrorMessage = "Date of birth is required")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Gender is required")]
    public Gender Gender { get; set; }

    [Required(ErrorMessage = "Marital status is required")]
    public MaritalStatus MaritalStatus { get; set; }

    [Required(ErrorMessage = "Mobile number is required")]
    [MaxLength(20, ErrorMessage = "Mobile number must not exceed 20 characters")]
    [Phone(ErrorMessage = "Invalid mobile number format")]
    public string MobileNumber { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "Email must not exceed 100 characters")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? EmailAddress { get; set; }

    [Required(ErrorMessage = "Document type is required")]
    public DocumentType DocumentType { get; set; }

    [Required(ErrorMessage = "Document number is required")]
    [MaxLength(50, ErrorMessage = "Document number must not exceed 50 characters")]
    public string DocumentNumber { get; set; } = string.Empty;

    public BloodGroup? BloodGroup { get; set; }

    [Required(ErrorMessage = "Religion is required")]
    public Religion Religion { get; set; }

    [Required(ErrorMessage = "Nationality is required")]
    public Nationality Nationality { get; set; }

    // Family Information
    [Required(ErrorMessage = "Father's name (English) is required")]
    [MaxLength(100, ErrorMessage = "Father's name must not exceed 100 characters")]
    public string FatherNameEnglish { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "Father's name (Bangla) must not exceed 100 characters")]
    public string? FatherNameBangla { get; set; }

    [Required(ErrorMessage = "Mother's name (English) is required")]
    [MaxLength(100, ErrorMessage = "Mother's name must not exceed 100 characters")]
    public string MotherNameEnglish { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "Mother's name (Bangla) must not exceed 100 characters")]
    public string? MotherNameBangla { get; set; }

    [MaxLength(100, ErrorMessage = "Spouse name must not exceed 100 characters")]
    public string? SpouseName { get; set; }

    [MaxLength(20, ErrorMessage = "Spouse mobile must not exceed 20 characters")]
    public string? SpouseMobile { get; set; }

    // Additional Information
    [MaxLength(50, ErrorMessage = "TIN number must not exceed 50 characters")]
    public string? TinNumber { get; set; }

    [MaxLength(100, ErrorMessage = "Employee reference must not exceed 100 characters")]
    public string? EmployeeReference { get; set; }
}
