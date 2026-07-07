using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Districts.Commands.CreateDistrict;

public class CreateDistrictCommand
{
    [Required(ErrorMessage = "District name is required")]
    [MaxLength(100, ErrorMessage = "District name must not exceed 100 characters")]
    public string DistrictName { get; set; } = string.Empty;

    [Required(ErrorMessage = "DivisionId is required")]
    public string DivisionId { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "District name (Bangla) must not exceed 100 characters")]
    public string? DistrictNameBangla { get; set; }
}
