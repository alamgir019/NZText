namespace NZ.HRM.Application.Divisions.Queries;

public class GetAllDivisionsQuery { }

public class DivisionDto
{
    public string Id { get; set; } = string.Empty;
    public string DivisionName { get; set; } = string.Empty;
    public string? DivisionNameBangla { get; internal set; }
}
