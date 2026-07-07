using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Thanas.Commands.UpdateThana;

public class UpdateThanaCommand
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "Thana name is required")]
    [MaxLength(100, ErrorMessage = "Thana name must not exceed 100 characters")]
    public string ThanaName { get; set; } = string.Empty;

    public string? DistrictId { get; set; }

    [MaxLength(100, ErrorMessage = "Thana name (Bangla) must not exceed 100 characters")]
    public string? ThanaNameBangla { get; set; }
}
