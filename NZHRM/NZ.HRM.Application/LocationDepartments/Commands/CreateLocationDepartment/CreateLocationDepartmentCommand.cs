using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.LocationDepartments.Commands.CreateLocationDepartment;

public class CreateLocationDepartmentCommand
{
    [Required]
    public string DepartmentId { get; set; } = string.Empty;

    [Required]
    public string UnitId { get; set; } = string.Empty;

    [Required]
    public string ComplexId { get; set; } = string.Empty;
}
