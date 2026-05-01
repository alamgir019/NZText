using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.EmployeePersonals.Queries.GetEmployeePersonalById;

public class EmployeePersonalDetailDto
{
    public string Id { get; set; } = string.Empty;
    public string EmployeeId { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;
    public string EmployeeNameEnglish { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public string MobileNumber { get; set; } = string.Empty;
    public string? EmailAddress { get; set; }
    public DocumentType DocumentType { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public BloodGroup? BloodGroup { get; set; }
    public Religion Religion { get; set; }
    public Nationality Nationality { get; set; }
    public string FatherNameEnglish { get; set; } = string.Empty;
    public string? FatherNameBangla { get; set; }
    public string MotherNameEnglish { get; set; } = string.Empty;
    public string? MotherNameBangla { get; set; }
    public string? SpouseName { get; set; }
    public string? SpouseMobile { get; set; }
    public string? TinNumber { get; set; }
    public string? EmployeeReference { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime UpdatedOn { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
}
