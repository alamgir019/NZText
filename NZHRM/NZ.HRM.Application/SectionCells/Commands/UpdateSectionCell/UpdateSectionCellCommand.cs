using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.SectionCells.Commands.UpdateSectionCell;

public class UpdateSectionCellCommand
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "Section ID is required")]
    public string SectionId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cell ID is required")]
    public string CellId { get; set; } = string.Empty;
}
