using NZ.HRM.Utility.Enum;
using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Model.Employees.Commands.CreateCompleteEmployee;

public class CreateDirectorReviewCommand
{
    // Basic Employment Information
    [Required(ErrorMessage = "Employee ID is required")]
    public string EmployeeId { get; set; } = string.Empty;

    public decimal? GrossSalary { get; set; }
    public decimal? ProposedMonthlySalary { get; set; }
    public string? EmployeeStatus { get; set; }

}