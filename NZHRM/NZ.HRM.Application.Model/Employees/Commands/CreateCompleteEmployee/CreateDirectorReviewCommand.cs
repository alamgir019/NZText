using NZ.HRM.Utility.Enum;
using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Model.Employees.Commands.CreateCompleteEmployee;

public class CreateDirectorReviewCommand
{
    // Basic Employment Information
    [Required(ErrorMessage = "Employee ID is required")]
    public string EmployeeId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Employee nature is required")]
    [MaxLength(50, ErrorMessage = "Employee nature must not exceed 50 characters")]
    public EmployeeNature EmployeeNature { get; set; } = EmployeeNature.Worker;

    public decimal? GrossSalary { get; set; }
    public decimal? ProposedMonthlySalary { get; set; }

}