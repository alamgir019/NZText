namespace NZ.HRM.Application.GroupComplexes.Queries.GetAllGroupComplexes;

public class GetAllGroupComplexesQuery
{
    public bool IncludeInactive { get; set; }
}

public class GroupComplexDto
{
    public string Id { get; set; } = string.Empty;
    public string GroupId { get; set; } = string.Empty;
    public string ComplexCode { get; set; } = string.Empty;
    public string ComplexName { get; set; } = string.Empty;
}
