namespace NZ.HRM.Application.GroupComplexes.Queries.GetGroupComplexById;

public class GetGroupComplexByIdQuery
{
    public string Id { get; set; } = string.Empty;
}

public class GroupComplexDetailDto
{
    public string Id { get; set; } = string.Empty;
    public string GroupId { get; set; } = string.Empty;
    public string ComplexCode { get; set; } = string.Empty;
    public string ComplexName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
