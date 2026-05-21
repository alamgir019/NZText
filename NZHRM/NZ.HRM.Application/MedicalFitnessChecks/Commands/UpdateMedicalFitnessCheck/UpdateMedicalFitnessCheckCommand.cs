using NZ.HRM.Utility.Enum;
using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.MedicalFitnessChecks.Commands.UpdateMedicalFitnessCheck;

public class UpdateMedicalFitnessCheckCommand
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required]
    public string EmployeeId { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string EnrollmentId { get; set; } = string.Empty;

    public BloodGroup? BloodGroup { get; set; }

    public decimal? HeightCm { get; set; }

    public decimal? WeightKg { get; set; }

    public string? PhysicalExaminationDataJson { get; set; }

    public bool IsFit { get; set; }

    [MaxLength(1000)]
    public string? Remarks { get; set; }

    [Required]
    [MaxLength(100)]
    public string ExaminedByDoctor { get; set; } = string.Empty;

    public DateTime ExaminationDateTime { get; set; }
}
