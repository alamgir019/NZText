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

    public string? IdentificationSign { get; set; }

    public FitnessOption Fitness { get; set; }

    [MaxLength(1000)]
    public string? Remarks { get; set; }

    [Required]
    [MaxLength(100)]
    public string ExaminedByDoctor { get; set; } = string.Empty;

    public DateTime ExaminationDateTime { get; set; }
}
