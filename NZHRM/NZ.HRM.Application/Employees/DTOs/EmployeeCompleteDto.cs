using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Employees.DTOs;

public class EmployeeCompleteDto
{
    // From EmployeeMaster - Basic Info
    public string Id { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;
    public string EmployeeNameEnglish { get; set; } = string.Empty;
    public string? EmployeeNameBangla { get; set; }

    // From EmployeeMaster - Company & Organization
    public string CompanyId { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string DepartmentId { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public string SectionId { get; set; } = string.Empty;
    public string SectionName { get; set; } = string.Empty;
    public string GradeId { get; set; } = string.Empty;
    public string GradeName { get; set; } = string.Empty;

    // From EmployeeMaster - Employment Details
    public EmployeeType EmployeeType { get; set; }
    public string ShiftId { get; set; } = string.Empty;
    public string ShiftName { get; set; } = string.Empty;
    public EmployeeNature EmployeeNature { get; set; }
    public string? HolidayId { get; set; }
    public string? HolidayName { get; set; }
    public DateTime JoiningDate { get; set; }
    public DateTime? ConfirmationDate { get; set; }
    public EmployeeStatus Status { get; set; }

    // From EmployeePersonal - Personal Information
    public string? PersonalInfoId { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public MaritalStatus? MaritalStatus { get; set; }
    public string? MobileNumber { get; set; }
    public string? EmailAddress { get; set; }
    public DocumentType? DocumentType { get; set; }
    public string? DocumentNumber { get; set; }
    public BloodGroup? BloodGroup { get; set; }
    public Religion? Religion { get; set; }
    public Nationality? Nationality { get; set; }

    // From EmployeePersonal - Family Information
    public string? FatherNameEnglish { get; set; }
    public string? FatherNameBangla { get; set; }
    public string? MotherNameEnglish { get; set; }
    public string? MotherNameBangla { get; set; }
    public string? SpouseName { get; set; }
    public string? SpouseMobile { get; set; }

    // From EmployeePersonal - Additional Information
    public string? TinNumber { get; set; }
    public string? EmployeeReference { get; set; }

    // Audit Fields
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime UpdatedOn { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
