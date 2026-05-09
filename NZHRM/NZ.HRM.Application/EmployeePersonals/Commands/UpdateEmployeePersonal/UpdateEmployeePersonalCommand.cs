using System.ComponentModel.DataAnnotations;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.EmployeePersonals.Commands.UpdateEmployeePersonal;

public class UpdateEmployeePersonalCommand
{
    public string Id { get; set; } = string.Empty;

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

    [MaxLength(50, ErrorMessage = "Reference person ID must not exceed 50 characters")]
    public string? ReferencePersonId { get; set; }

    [MaxLength(200, ErrorMessage = "Permanent village/area/road must not exceed 200 characters")]
    public string? PermanentVillageAreaRoad { get; set; }

    [MaxLength(100, ErrorMessage = "Permanent post office must not exceed 100 characters")]
    public string? PermanentPostOffice { get; set; }

    [MaxLength(100, ErrorMessage = "Permanent thana must not exceed 100 characters")]
    public string? PermanentThana { get; set; }

    [MaxLength(100, ErrorMessage = "Permanent district must not exceed 100 characters")]
    public string? PermanentDistrict { get; set; }

    [MaxLength(100, ErrorMessage = "Permanent division must not exceed 100 characters")]
    public string? PermanentDivision { get; set; }

    [MaxLength(200, ErrorMessage = "Present village/area/road must not exceed 200 characters")]
    public string? PresentVillageAreaRoad { get; set; }

    [MaxLength(100, ErrorMessage = "Present post office must not exceed 100 characters")]
    public string? PresentPostOffice { get; set; }

    [MaxLength(100, ErrorMessage = "Present thana must not exceed 100 characters")]
    public string? PresentThana { get; set; }

    [MaxLength(100, ErrorMessage = "Present district must not exceed 100 characters")]
    public string? PresentDistrict { get; set; }

    [MaxLength(100, ErrorMessage = "Present division must not exceed 100 characters")]
    public string? PresentDivision { get; set; }
}
