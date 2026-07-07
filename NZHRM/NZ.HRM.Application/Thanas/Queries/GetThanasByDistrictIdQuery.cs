namespace NZ.HRM.Application.Thanas.Queries;

public class GetThanasByDistrictIdQuery
{
    public string DistrictId { get; set; } = string.Empty;
}

public class ThanaDto
{
    public string Id { get; set; } = string.Empty;
    public string ThanaName { get; set; } = string.Empty;
    public string DistrictId { get; set; } = string.Empty;
    public string DistrictName { get; set; } = string.Empty;
    public string? ThanaNameBangla { get; internal set; }
}
