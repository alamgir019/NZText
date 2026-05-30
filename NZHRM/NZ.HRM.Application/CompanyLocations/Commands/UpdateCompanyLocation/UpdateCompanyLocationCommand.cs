using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.CompanyLocations.Commands.UpdateCompanyLocation;

public class UpdateCompanyLocationCommand
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "Company ID is required")]
    public string CompanyId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Location ID is required")]
    public string LocationId { get; set; } = string.Empty;
}
