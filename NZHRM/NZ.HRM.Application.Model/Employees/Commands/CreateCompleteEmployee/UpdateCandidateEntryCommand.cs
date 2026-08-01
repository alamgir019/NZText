using System.ComponentModel.DataAnnotations;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Model.Employees.Commands.CreateCompleteEmployee;

public class UpdateCandidateEntryCommand
{
    [MaxLength(100, ErrorMessage = "Employee name (Bangla) must not exceed 100 characters")]
    public string? EmployeeNameBangla { get; set; }

    // Company & Organization
    [Required(ErrorMessage = "Unit ID is required")]
    public string UnitId { get; set; } = string.Empty;

    public string? DesignationId { get; set; }

    [Required(ErrorMessage = "Joining date is required")]
    public DateOnly JoiningDate { get; set; }

    // Personal Information
    [Required(ErrorMessage = "Date of birth is required")]
    public DateOnly DateOfBirth { get; set; }

    [Required(ErrorMessage = "Gender is required")]
    public Gender Gender { get; set; }


    [Required(ErrorMessage = "Religion is required")]
    public Religion Religion { get; set; }
    public BloodGroup? BloodGroup { get; set; }
    public IDType IDType { get; set; }
    public string? IDNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mobile number is required")]
    [MaxLength(20, ErrorMessage = "Mobile number must not exceed 20 characters")]
    public string MobileNumber { get; set; } = string.Empty;

    // Family Information
    public GuardianType? GuardianType { get; set; }
    [MaxLength(100, ErrorMessage = "Guardian's name (Bangla) must not exceed 100 characters")]
    public string? GuardianNameBangla { get; set; }

    [MaxLength(100, ErrorMessage = "Father's name (Bangla) must not exceed 100 characters")]
    public string FatherNameBangla { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "Mother's name (Bangla) must not exceed 100 characters")]
    public string? MotherNameBangla { get; set; }

    // Address Information
    [MaxLength(200, ErrorMessage = "Permanent village/area/road must not exceed 200 characters")]
    public string? PermanentVillageAreaRoad { get; set; }

    [MaxLength(100, ErrorMessage = "Permanent post office must not exceed 100 characters")]
    public string? PermanentPostOffice { get; set; }

    [MaxLength(100, ErrorMessage = "Permanent thana must not exceed 100 characters")]
    public string? PermanentThanaId { get; set; }

    [MaxLength(100, ErrorMessage = "Permanent district must not exceed 100 characters")]
    public string? PermanentDistrictId { get; set; }

    [MaxLength(100, ErrorMessage = "Permanent division must not exceed 100 characters")]
    public string? PermanentDivisionId { get; set; }


    [MaxLength(200, ErrorMessage = "Present village/area/road must not exceed 200 characters")]
    public string? PresentVillageAreaRoad { get; set; }

    [MaxLength(100, ErrorMessage = "Present post office must not exceed 100 characters")]
    public string? PresentPostOffice { get; set; }

    [MaxLength(100, ErrorMessage = "Present thana must not exceed 100 characters")]
    public string? PresentThanaId { get; set; }

    [MaxLength(100, ErrorMessage = "Present district must not exceed 100 characters")]
    public string? PresentDistrictId { get; set; }

    [MaxLength(100, ErrorMessage = "Present division must not exceed 100 characters")]
    public string? PresentDivisionId { get; set; }
    public string? NomineeNameBangla { get; set; }
    public RelationshipType? NomineeRelation { get; set; }
}
