using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.MedicalFitnessChecks.Queries.GetMedicalFitnessReportByEmployeeId;

public class MedicalFitnessReportDto
{
    public string EmployeeId { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;
    public string? EnrollmentId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string? FatherName { get; set; }
    public string? MotherName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public string? MobileNumber { get; set; }

    public string CompanyName { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public string SectionName { get; set; } = string.Empty;
    public string DesignationName { get; set; } = string.Empty;

    public string? PresentVillageAreaRoad { get; set; }
    public string? PresentPostOffice { get; set; }
    public string? PresentThana { get; set; }
    public string? PresentDistrict { get; set; }
    public string? PresentDivision { get; set; }

    public string MedicalFitnessCheckId { get; set; } = string.Empty;
    public BloodGroup? BloodGroupTested { get; set; }
    public decimal? HeightCm { get; set; }
    public decimal? WeightKg { get; set; }
    public string? PhysicalExaminationDataJson { get; set; }
    public bool IsFit { get; set; }
    public string? Remarks { get; set; }
    public string ExaminedByDoctor { get; set; } = string.Empty;
    public DateTime ExaminationDateTime { get; set; }
}
