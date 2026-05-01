using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Sections.Commands.UpdateSection;

public class UpdateSectionCommand
{
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department ID is required")]
    public string DepartmentId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Section name is required")]
    [MaxLength(100, ErrorMessage = "Section name must not exceed 100 characters")]
    public string SectionName { get; set; } = string.Empty;
}
