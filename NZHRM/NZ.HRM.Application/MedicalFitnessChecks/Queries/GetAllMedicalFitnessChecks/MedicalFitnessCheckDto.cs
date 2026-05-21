using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.MedicalFitnessChecks.Queries.GetAllMedicalFitnessChecks;

public class MedicalFitnessCheckDto
{
    public string Id { get; set; } = string.Empty;
    public string EmployeeId { get; set; } = string.Empty;
    public string TemporaryCandidateId { get; set; } = string.Empty;
    public string? CandidateName { get; set; }
    public string? MobileNumber { get; set; }
    public BloodGroup? BloodGroup { get; set; }
    public decimal? HeightCm { get; set; }
    public decimal? WeightKg { get; set; }
    public string? PhysicalExaminationDataJson { get; set; }
    public bool IsFit { get; set; }
    public string? Remarks { get; set; }
    public string ExaminedByDoctor { get; set; } = string.Empty;
    public DateTime ExaminationDateTime { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime UpdatedOn { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
