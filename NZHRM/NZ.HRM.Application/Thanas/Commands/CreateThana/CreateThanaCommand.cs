using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Thanas.Commands.CreateThana;

public class CreateThanaCommand
{
    [Required(ErrorMessage = "Thana name is required")]
    [MaxLength(100, ErrorMessage = "Thana name must not exceed 100 characters")]
    public string ThanaName { get; set; } = string.Empty;

    [Required(ErrorMessage = "DistrictId is required")]
    public string DistrictId { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "Thana name (Bangla) must not exceed 100 characters")]
    public string? ThanaNameBangla { get; set; }
}
