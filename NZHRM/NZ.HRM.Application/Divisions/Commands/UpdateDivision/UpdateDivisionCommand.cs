using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Divisions.Commands.UpdateDivision;

public class UpdateDivisionCommand
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "Division name is required")]
    [MaxLength(100, ErrorMessage = "Division name must not exceed 100 characters")]
    public string DivisionName { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "Division name (Bangla) must not exceed 100 characters")]
    public string? DivisionNameBangla { get; set; }
}
