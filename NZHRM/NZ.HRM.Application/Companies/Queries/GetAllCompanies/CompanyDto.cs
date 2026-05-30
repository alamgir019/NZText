namespace NZ.HRM.Application.Companies.Queries.GetAllCompanies;

public class CompanyDto
{
    public string Id { get; set; } = string.Empty;
    public string CompanyCode { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    // Locations are available via CompanyLocation mapping
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime UpdatedOn { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsCompliant { get; set; }
}