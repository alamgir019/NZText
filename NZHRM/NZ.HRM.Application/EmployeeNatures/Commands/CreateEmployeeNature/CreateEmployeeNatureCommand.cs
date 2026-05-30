using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.EmployeeNatures.Commands.CreateEmployeeNature;

public class CreateEmployeeNatureCommand
{
    [Required(ErrorMessage = "Employee nature name is required")]
    [MaxLength(100, ErrorMessage = "Employee nature name must not exceed 100 characters")]
    public string NatureName { get; set; } = string.Empty;

    public int SortOrder { get; set; } = 1;
}
