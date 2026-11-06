using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.EmployeeShiftChanges.Commands.GenerateAnnualShiftRotation;

public class GenerateAnnualShiftRotationCommand
{
    [Range(2000, 9999, ErrorMessage = "Year must be between 2000 and 9999")]
    public int Year { get; set; }
}
