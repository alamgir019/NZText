using NZ.HRM.Utility.Enum;
using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.MedicalFitnessChecks.Commands.CreateMedicalFitnessCheck;

public class CreateMedicalFitnessCheckCommand
{
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
