using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Cells.Commands.UpdateCell;

public class UpdateCellCommand
{
    public string Id { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string NameEnglish { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? NameBangla { get; set; }

    [Required]
    public string SectionId { get; set; } = string.Empty;
}
