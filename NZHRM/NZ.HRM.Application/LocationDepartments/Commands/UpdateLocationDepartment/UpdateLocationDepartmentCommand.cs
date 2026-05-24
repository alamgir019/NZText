using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.LocationDepartments.Commands.UpdateLocationDepartment;

public class UpdateLocationDepartmentCommand
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "Location ID is required")]
    public string LocationId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department ID is required")]
    public string DepartmentId { get; set; } = string.Empty;
}
