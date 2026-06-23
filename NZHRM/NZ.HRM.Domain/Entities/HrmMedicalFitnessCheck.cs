using NZ.HRM.Domain.Common;
using NZ.HRM.Utility.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.HRM.Domain.Entities;

[Table("medical_fitness_check", Schema = "hrm")]
public class HrmMedicalFitnessCheck : BaseEntity
{
    [Required]
    public string EmployeeId { get; set; } = string.Empty;

    [ForeignKey(nameof(EmployeeId))]
    public HrmEmployeeMaster? Employee { get; set; }

    [Required]
    [MaxLength(50)]
    public string EnrollmentId { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? IdentificationSign { get; set; }

    public string Fitness { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Remarks { get; set; }

    [Required]
    [MaxLength(100)]
    public string ExaminedByDoctor { get; set; } = string.Empty;

    public DateTime ExaminationDateTime { get; set; }
}
