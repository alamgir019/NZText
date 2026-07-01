using System.ComponentModel.DataAnnotations;
using NZ.HRM.Application.Model.Employees.DTOs;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Model.Employees.Commands.CreateCompleteEmployee;

public class CreateEmployeeHRExecutiveCommand
{
    // Basic Employment Information
    [Required(ErrorMessage = "Employee ID is required")]
    public string EmployeeId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Employee enrollment ID is required")]
    [MaxLength(50, ErrorMessage = "Employee enrollment ID must not exceed 50 characters")]
    public string EmployeeEnrollmentId { get; set; } = string.Empty;

    // Company & Organization
    [Required(ErrorMessage = "Unit ID is required")]
    public string UnitId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Subunit ID is required")]
    public string SubunitId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department ID is required")]
    public string DepartmentId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Section ID is required")]
    public string SectionId { get; set; } = string.Empty;
    public string? CellId { get; set; }

    public string? DesignationId { get; set; }

    public string? GradeId { get; set; }

    // Employment Details
    public EmployeeType? EmployeeType { get; set; }
    public string? EmployeeTypeId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Shift is required")]
    public string ShiftId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Employee nature is required")]
    public string? EmployeeNatureId { get; set; }

    public WeekOffDay Holiday { get; set; }


    [Required(ErrorMessage = "Joining date is required")]
    public DateOnly JoiningDate { get; set; }

    //salary information

    public decimal? ProposedMonthlySalary { get; set; }
    public decimal? BankPortion { get; set; }
    public decimal? CashPortion { get; set; }
    public Dictionary<string, decimal> OtherAllowance { get; set; } = new Dictionary<string, decimal>();
    public decimal? Tax { get; set; }
    public string PaymentMethod { get; set; } = string.Empty; // e.g., "Bank Transfer", "Cash", etc.
    public string? BankingId { get; set; }
    public string? AccountName { get; set; }
    public string? AccountNo { get; set; }
    public string? RoutingNo { get; set; }
    public string? BranchName { get; set; }
    public bool SalaryAccountFlag { get; set; }
    public string? AccountType { get; set; } // e.g., "Savings", "Current", etc.

    // Personal Information
    //[Required(ErrorMessage = "Date of birth is required")]
    //public DateOnly DateOfBirth { get; set; }

    //[Required(ErrorMessage = "Gender is required")]
    //public Gender Gender { get; set; }

    //[Required(ErrorMessage = "Marital status is required")]
    //public MaritalStatus MaritalStatus { get; set; }

    //[Required(ErrorMessage = "Mobile number is required")]
    //[MaxLength(20, ErrorMessage = "Mobile number must not exceed 20 characters")]
    //public string MobileNumber { get; set; } = string.Empty;

    //[MaxLength(100, ErrorMessage = "Email must not exceed 100 characters")]
    //[EmailAddress(ErrorMessage = "Invalid email format")]
    //public string? EmailAddress { get; set; }

    public List<EmployeeDocumentDto>? Documents { get; set; }

    //public BloodGroup? BloodGroup { get; set; }

    //[Required(ErrorMessage = "Religion is required")]
    //public Religion Religion { get; set; }

    //[Required(ErrorMessage = "Nationality is required")]
    //public Nationality Nationality { get; set; }

    //// Family Information
    //[Required(ErrorMessage = "Father's name (English) is required")]
    //[MaxLength(100, ErrorMessage = "Father's name must not exceed 100 characters")]
    //public string FatherNameEnglish { get; set; } = string.Empty;

    //[MaxLength(100, ErrorMessage = "Father's name (Bangla) must not exceed 100 characters")]
    //public string FatherNameBangla { get; set; } = string.Empty;

    //[Required(ErrorMessage = "Mother's name (English) is required")]
    //[MaxLength(100, ErrorMessage = "Mother's name must not exceed 100 characters")]
    //public string MotherNameEnglish { get; set; } = string.Empty;

    //[MaxLength(100, ErrorMessage = "Mother's name (Bangla) must not exceed 100 characters")]
    //public string MotherNameBangla { get; set; } = string.Empty;

    //[MaxLength(100, ErrorMessage = "Spouse name must not exceed 100 characters")]
    //public string SpouseName { get; set; } = string.Empty;

    //[MaxLength(20, ErrorMessage = "Spouse mobile must not exceed 20 characters")]
    //public string? SpouseMobile { get; set; }

    // Additional Information
    [MaxLength(50, ErrorMessage = "TIN number must not exceed 50 characters")]
    public string? TinNumber { get; set; }
    public decimal? ProbationPeriod { get; set; }
    public string? ReportingTo { get; set; }
    public string? ProcessingGroupId { get; set; }
    public decimal? GrossSalary { get; set; }

    //[MaxLength(100, ErrorMessage = "Employee reference must not exceed 100 characters")]
    //public string? EmployeeReference { get; set; }

    //[MaxLength(50, ErrorMessage = "Reference person ID must not exceed 50 characters")]
    //public string? ReferencePersonId { get; set; }

    //// Address Information
    //[MaxLength(200, ErrorMessage = "Permanent village/area/road must not exceed 200 characters")]
    //public string? PermanentVillageAreaRoad { get; set; }

    //[MaxLength(100, ErrorMessage = "Permanent post office must not exceed 100 characters")]
    //public string? PermanentPostOffice { get; set; }

    //[MaxLength(100, ErrorMessage = "Permanent thana must not exceed 100 characters")]
    //public string? PermanentThana { get; set; }

    //[MaxLength(100, ErrorMessage = "Permanent district must not exceed 100 characters")]
    //public string? PermanentDistrict { get; set; }

    //[MaxLength(100, ErrorMessage = "Permanent division must not exceed 100 characters")]
    //public string? PermanentDivision { get; set; }

    //[MaxLength(200, ErrorMessage = "Present village/area/road must not exceed 200 characters")]
    //public string? PresentVillageAreaRoad { get; set; }

    //[MaxLength(100, ErrorMessage = "Present post office must not exceed 100 characters")]
    //public string? PresentPostOffice { get; set; }

    //[MaxLength(100, ErrorMessage = "Present thana must not exceed 100 characters")]
    //public string? PresentThana { get; set; }

    //[MaxLength(100, ErrorMessage = "Present district must not exceed 100 characters")]
    //public string? PresentDistrict { get; set; }

    //[MaxLength(100, ErrorMessage = "Present division must not exceed 100 characters")]
    //public string? PresentDivision { get; set; }

    // Verification Information
    //[MaxLength(100, ErrorMessage = "Security clearance by must not exceed 100 characters")]
    //public string? SecurityClearanceBy { get; set; }

    //public DateOnly? SecurityClearanceDate { get; set; }

    //[MaxLength(100, ErrorMessage = "Enrolled by must not exceed 100 characters")]
    //public string? EnrolledBy { get; set; }

    //public DateOnly? EnrolledDate { get; set; }

    //[MaxLength(100, ErrorMessage = "Biometric enrolled by must not exceed 100 characters")]
    //public string? BiometricEnrolledBy { get; set; }

    //public DateOnly? BiometricEnrolledDate { get; set; }
}
