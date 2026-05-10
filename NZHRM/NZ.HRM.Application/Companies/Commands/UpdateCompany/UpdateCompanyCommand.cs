using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Companies.Commands.UpdateCompany;

public class UpdateCompanyCommand
{
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "Company code is required")]
    [MaxLength(10, ErrorMessage = "Company code must not exceed 10 characters")]
    public string CompanyCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Company name is required")]
    [MaxLength(100, ErrorMessage = "Company name must not exceed 100 characters")]
    public string CompanyName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Location ID is required")]
    public string LocationId { get; set; } = string.Empty;

    public bool IsCompliant { get; set; } = false;
}