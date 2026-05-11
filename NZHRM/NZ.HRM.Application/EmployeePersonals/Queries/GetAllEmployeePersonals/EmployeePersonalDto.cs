using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.EmployeePersonals.Queries.GetAllEmployeePersonals;

public class EmployeePersonalDto
{
    public string Id { get; set; } = string.Empty;
    public string EmployeeId { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;
    public string EmployeeNameEnglish { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public MaritalStatus? MaritalStatus { get; set; }
    public string MobileNumber { get; set; } = string.Empty;
    public string? EmailAddress { get; set; }
    public DocumentType? DocumentType { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public BloodGroup? BloodGroup { get; set; }
    public Religion? Religion { get; set; }
    public Nationality? Nationality { get; set; }
    public string FatherNameEnglish { get; set; } = string.Empty;
    public string? FatherNameBangla { get; set; }
    public string MotherNameEnglish { get; set; } = string.Empty;
    public string? MotherNameBangla { get; set; }
    public string? SpouseName { get; set; }
    public string? SpouseMobile { get; set; }
    public string? TinNumber { get; set; }
    public string? EmployeeReference { get; set; }
    public string? ReferencePersonId { get; set; }
    public string? PermanentVillageAreaRoad { get; set; }
    public string? PermanentPostOffice { get; set; }
    public string? PermanentThana { get; set; }
    public string? PermanentDistrict { get; set; }
    public string? PermanentDivision { get; set; }
    public string? PresentVillageAreaRoad { get; set; }
    public string? PresentPostOffice { get; set; }
    public string? PresentThana { get; set; }
    public string? PresentDistrict { get; set; }
    public string? PresentDivision { get; set; }
}
