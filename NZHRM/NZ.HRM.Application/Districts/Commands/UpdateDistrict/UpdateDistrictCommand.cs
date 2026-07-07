using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Districts.Commands.UpdateDistrict;

public class UpdateDistrictCommand
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "District name is required")]
    [MaxLength(100, ErrorMessage = "District name must not exceed 100 characters")]
    public string DistrictName { get; set; } = string.Empty;

    public string? DivisionId { get; set; }

    [MaxLength(100, ErrorMessage = "District name (Bangla) must not exceed 100 characters")]
    public string? DistrictNameBangla { get; set; }
}
