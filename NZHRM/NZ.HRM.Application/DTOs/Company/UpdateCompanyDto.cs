using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.DTOs.Company;

public class UpdateCompanyDto
{
    [Required(ErrorMessage = "Company name is required")]
    [MaxLength(100, ErrorMessage = "Company name must not exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200, ErrorMessage = "Address must not exceed 200 characters")]
    public string? Address { get; set; }

    [MaxLength(20, ErrorMessage = "Phone must not exceed 20 characters")]
    public string? Phone { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email format")]
    [MaxLength(100, ErrorMessage = "Email must not exceed 100 characters")]
    public string? Email { get; set; }

    [MaxLength(100, ErrorMessage = "Website must not exceed 100 characters")]
    public string? Website { get; set; }
}