using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Grades.Commands.CreateGrade;

public class CreateGradeCommand
{
    [Required(ErrorMessage = "Grade name is required")]
    [MaxLength(50, ErrorMessage = "Grade name must not exceed 50 characters")]
    public string GradeName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Minimum salary is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Minimum salary must be greater than or equal to 0")]
    public decimal MinSalary { get; set; }

    [Required(ErrorMessage = "Maximum salary is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Maximum salary must be greater than or equal to 0")]
    public decimal MaxSalary { get; set; }

    [MaxLength(50, ErrorMessage = "Employee type must not exceed 50 characters")]
    public string EmployeeType { get; set; } = string.Empty;
}
