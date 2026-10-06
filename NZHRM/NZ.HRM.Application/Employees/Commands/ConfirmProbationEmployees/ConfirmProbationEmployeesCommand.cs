using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Employees.Commands.ConfirmProbationEmployees;

public class ConfirmProbationEmployeesCommand
{
    [Required]
    [MinLength(1, ErrorMessage = "At least one employee must be selected for confirmation.")]
    public List<string> EmployeeIds { get; set; } = new();
}
