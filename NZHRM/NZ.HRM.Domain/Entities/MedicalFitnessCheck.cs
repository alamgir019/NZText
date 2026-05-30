using NZ.HRM.Domain.Common;
using NZ.HRM.Utility.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.HRM.Domain.Entities;

public class MedicalFitnessCheck : BaseEntity
{
    [Required]
    public string EmployeeId { get; set; } = string.Empty;

    [ForeignKey(nameof(EmployeeId))]
    public EmployeeMaster? Employee { get; set; }

    [Required]
    [MaxLength(50)]
    public string EnrollmentId { get; set; } = string.Empty;

    [MaxLength(20)]
    public BloodGroup? BloodGroup { get; set; }

    public decimal? HeightCm { get; set; }

    public decimal? WeightKg { get; set; }

    [MaxLength(2000)]
    public string? PhysicalExaminationDataJson { get; set; }

    public bool IsFit { get; set; }

    [MaxLength(1000)]
    public string? Remarks { get; set; }

    [Required]
    [MaxLength(100)]
    public string ExaminedByDoctor { get; set; } = string.Empty;

    public DateTime ExaminationDateTime { get; set; }
}
