using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.LocationDepartments.Commands.UpdateLocationDepartment;

public class UpdateLocationDepartmentCommand
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required]
    public string DepartmentId { get; set; } = string.Empty;

    [Required]
    public string UnitId { get; set; } = string.Empty;

    [Required]
    public string ComplexId { get; set; } = string.Empty;
}
