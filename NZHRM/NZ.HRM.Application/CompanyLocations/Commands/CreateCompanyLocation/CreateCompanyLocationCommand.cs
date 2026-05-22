using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.CompanyLocations.Commands.CreateCompanyLocation;

public class CreateCompanyLocationCommand
{
    [Required(ErrorMessage = "Company ID is required")]
    public string CompanyId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Location ID is required")]
    public string LocationId { get; set; } = string.Empty;
}
