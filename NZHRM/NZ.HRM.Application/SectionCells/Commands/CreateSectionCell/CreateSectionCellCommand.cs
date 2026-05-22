using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.SectionCells.Commands.CreateSectionCell;

public class CreateSectionCellCommand
{
    [Required(ErrorMessage = "Section ID is required")]
    public string SectionId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cell ID is required")]
    public string CellId { get; set; } = string.Empty;
}
