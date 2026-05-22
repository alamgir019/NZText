using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.DepartmentSections.Commands.UpdateDepartmentSection;

public class UpdateDepartmentSectionCommand
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department ID is required")]
    public string DepartmentId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Section ID is required")]
    public string SectionId { get; set; } = string.Empty;
}
