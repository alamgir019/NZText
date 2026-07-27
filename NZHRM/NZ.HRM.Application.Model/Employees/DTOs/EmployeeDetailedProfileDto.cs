using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Model.Employees.DTOs;

/// <summary>
/// Comprehensive employee detailed profile with all sections
/// </summary>
public class EmployeeDetailedProfileDto
{
    #region Left Panel Information
    public string EmployeeId { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;
    public string? DateOfJoining { get; set; }
    public string? EmploymentType { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    #endregion

    #region Personal Information
    public string FullName { get; set; } = string.Empty;
    public string? FatherName { get; set; }
    public string? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public BloodGroup? BloodGroup { get; set; }
    public Religion? Religion { get; set; }
    public string? Nationality { get; set; }
    public string? IDNumber { get; set; }
    public string? Mobile { get; set; }
    public string? PhotoUrl { get; set; }
    #endregion

    #region Service Information
    public string? Company { get; set; }
    public string? Department { get; set; }
    public string? Section { get; set; }
    public string? Cell { get; set; }
    public string? Designation { get; set; }
    public string? Grade { get; set; }
    public string? Shift { get; set; }
    public string? WeeklyOff { get; set; }
    public string? ReportingTo { get; set; }
    #endregion

    #region Salary Information
    public decimal? BasicSalary { get; set; }
    public decimal? HouseRent { get; set; }
    public string? OtherAllowances { get; set; }
    public decimal? GrossSalary { get; set; }
    public decimal? MonthlySalary { get; set; }
    #endregion

    #region Address Information
    public AddressInformationDto? PresentAddress { get; set; }
    public AddressInformationDto? PermanentAddress { get; set; }
    #endregion

    #region Nominee Information
    public NomineeInformationDto? NomineeInfo { get; set; }
    #endregion

    #region Medical Information
    public MedicalInformationDto? MedicalInfo { get; set; }
    #endregion

    #region Documents Summary
    public List<DocumentSummaryDto> Documents { get; set; } = new();
    #endregion

    #region Appointment Letter
    public string? AppointmentLetterDetails { get; set; }
    #endregion

    #region Promotion / Transfer History
    public List<PromotionTransferHistoryDto> PromotionTransferHistory { get; set; } = new();
    public decimal? MedicalAllowance { get; set; }
    public decimal? FoodAllowance { get; set; }
    public decimal? ConveyanceAllowance { get; set; }
    public string? EnrollmentId { get; set; }
    #endregion
}

public class AddressInformationDto
{
    public string? VillageAreaRoad { get; set; }
    public string? PostOffice { get; set; }
    public string? ThanaName { get; set; }
    public string? DistrictName { get; set; }
    public string? DivisionName { get; set; }
}

public class NomineeInformationDto
{
    public string? NomineeName { get; set; }
    public string? Relation { get; set; }
    public string? Mobile { get; set; }
    public string? Address { get; set; }
}

public class MedicalInformationDto
{
    public string? MedicalStatus { get; set; }
    public string? DateOfMedical { get; set; }
    public string? MedicalCenter { get; set; }
    public string? BloodGroupMedical { get; set; }
}

public class DocumentSummaryDto
{
    public DocumentType? DocumentType { get; set; }
    public string? Status { get; set; }
    public bool IsAvailable { get; set; }
    public string? FilePath { get; set; }
}

public class PromotionTransferHistoryDto
{
    public string? Date { get; set; }
    public string? Type { get; set; }
    public string? From { get; set; }
    public string? To { get; set; }
    public string? Remarks { get; set; }
}
