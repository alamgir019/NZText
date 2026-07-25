using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Cells.Commands.CreateCell;

public class CreateCellCommand
{
    [Required]
    [MaxLength(100)]
    public string CellName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? CellNameBangla { get; set; }

    [Required]
    public string SectionId { get; set; } = string.Empty;
}
