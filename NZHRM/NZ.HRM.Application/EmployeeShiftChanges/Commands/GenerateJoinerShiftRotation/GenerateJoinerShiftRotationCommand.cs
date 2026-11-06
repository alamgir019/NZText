using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.EmployeeShiftChanges.Commands.GenerateJoinerShiftRotation;

public class GenerateJoinerShiftRotationCommand
{
    [Required(ErrorMessage = "Employee ID is required")]
    public string EmployeeId { get; set; } = string.Empty;

    [Range(2000, 9999, ErrorMessage = "Year must be between 2000 and 9999")]
    public int? Year { get; set; }
}
