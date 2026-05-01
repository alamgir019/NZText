using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Departments.Commands.UpdateDepartment;

public class UpdateDepartmentCommand
{
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department name is required")]
    [MaxLength(100, ErrorMessage = "Department name must not exceed 100 characters")]
    public string DepartmentName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department code is required")]
    [MaxLength(10, ErrorMessage = "Department code must not exceed 10 characters")]
    public string DepartmentCode { get; set; } = string.Empty;
}
