using System.ComponentModel.DataAnnotations;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Model.Employees.Commands.CreateCompleteEmployee;

public class CreateCandidateEntryCommand
{
    // Basic Employment Information
    [Required(ErrorMessage = "Employee enrollment ID is required")]
    [MaxLength(50, ErrorMessage = "Employee enrollment ID must not exceed 50 characters")]
    public string EmployeeEnrollmentId { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "Employee name (Bangla) must not exceed 100 characters")]
    public string? EmployeeNameBangla { get; set; }

    // Employment Details
    [Required(ErrorMessage = "Employee type is required")]
    public EmployeeType EmployeeType { get; set; }

    // Company & Organization
    [Required(ErrorMessage = "Unit ID is required")]
    public string UnitId { get; set; } = string.Empty;

    //[Required(ErrorMessage = "Department ID is required")]
    public string? DepartmentId { get; set; } = string.Empty;

    //[Required(ErrorMessage = "Location ID is required")]
    public string? LocationId { get; set; } = string.Empty;

    //[Required(ErrorMessage = "Section ID is required")]
    public string? SectionId { get; set; } = string.Empty;

    public string? CellId { get; set; }

    public decimal? ProposedMonthlySalary { get; set; }

    [Required(ErrorMessage = "Joining date is required")]
    public DateOnly JoiningDate { get; set; }

    public DateOnly? ConfirmationDate { get; set; }

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
    public GuardianType GuardianType { get; set; }
    [MaxLength(100, ErrorMessage = "Guardian's name (Bangla) must not exceed 100 characters")]
    public string GuardianName { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "Mother's name (Bangla) must not exceed 100 characters")]
    public string? MotherNameBangla { get; set; }
    public ReferenceType? ReferenceType { get; set; }

    [MaxLength(100, ErrorMessage = "Employee reference must not exceed 100 characters")]
    public string? EmployeeReference { get; set; }

    [MaxLength(50, ErrorMessage = "Reference person ID must not exceed 50 characters")]
    public string? ReferencePersonId { get; set; }

    [MaxLength(20, ErrorMessage = "Mobile number must not exceed 20 characters")]
    public string? ReferenceMobileNumber { get; set; }
    public Relation? Relationship { get; set; }



    // Address Information
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

    // Verification Information
    [MaxLength(100, ErrorMessage = "Security clearance by must not exceed 100 characters")]
    public string? SecurityClearanceBy { get; set; }

    public DateOnly? SecurityClearanceDate { get; set; }

    [MaxLength(100, ErrorMessage = "Enrolled by must not exceed 100 characters")]
    public string? EnrolledBy { get; set; }

    public DateOnly? EnrolledDate { get; set; }

    [MaxLength(100, ErrorMessage = "Biometric enrolled by must not exceed 100 characters")]
    public string? BiometricEnrolledBy { get; set; }

    public DateOnly? BiometricEnrolledDate { get; set; }
}
