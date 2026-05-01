using System.ComponentModel.DataAnnotations;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.EmployeePersonals.Commands.CreateEmployeePersonal;

public class CreateEmployeePersonalCommand
{
    [Required(ErrorMessage = "Employee ID is required")]
    public string EmployeeId { get; set; } = string.Empty;

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

    [MaxLength(50, ErrorMessage = "TIN number must not exceed 50 characters")]
    public string? TinNumber { get; set; }

    [MaxLength(100, ErrorMessage = "Employee reference must not exceed 100 characters")]
    public string? EmployeeReference { get; set; }
}
