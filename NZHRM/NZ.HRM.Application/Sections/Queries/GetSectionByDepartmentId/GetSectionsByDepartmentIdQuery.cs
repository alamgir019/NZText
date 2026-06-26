namespace NZ.HRM.Application.Sections.Queries.GetSectionsByDepartmentId;

public class GetSectionsByDepartmentIdQuery
{
    public string DepartmentId { get; set; } = string.Empty;
    public bool IncludeInactive { get; set; } = false;
}