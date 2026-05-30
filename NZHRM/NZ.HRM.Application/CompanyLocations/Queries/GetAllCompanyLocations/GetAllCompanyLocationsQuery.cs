namespace NZ.HRM.Application.CompanyLocations.Queries.GetAllCompanyLocations;

public class GetAllCompanyLocationsQuery
{
    public bool IncludeInactive { get; set; } = false;
    public string? CompanyId { get; set; }
    public string? LocationId { get; set; }
}
