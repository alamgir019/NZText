namespace NZ.HRM.Application.Sections.Queries.GetAllSections;

public class GetAllSectionsQuery
{
    public bool IncludeInactive { get; set; } = false;
    public string? DepartmentId { get; set; }
}
