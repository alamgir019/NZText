namespace NZ.HRM.Application.Districts.Queries;

public class GetDistrictsByDivisionIdQuery
{
    public string DivisionId { get; set; } = string.Empty;
}

public class DistrictDto
{
    public string Id { get; set; } = string.Empty;
    public string DistrictName { get; set; } = string.Empty;
    public string DivisionId { get; set; } = string.Empty;
    public string DivisionName { get; set; } = string.Empty;
}
