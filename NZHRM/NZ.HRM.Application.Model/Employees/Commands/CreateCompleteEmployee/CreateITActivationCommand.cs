using NZ.HRM.Application.Model.Employees.DTOs;
using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Model.Employees.Commands.CreateCompleteEmployee;

public class CreateITActivationCommand
{
    // Basic Employment Information
    [Required(ErrorMessage = "Employee ID is required")]
    public string EmployeeId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Employee enrollment ID is required")]
    [MaxLength(50, ErrorMessage = "Employee enrollment ID must not exceed 50 characters")]
    public string EmployeeEnrollmentId { get; set; } = string.Empty;
    public List<EmployeeDocumentDto> Documents { get; set; } = new List<EmployeeDocumentDto>();
    public string EmployeeCode { get; set; } = string.Empty;
}

