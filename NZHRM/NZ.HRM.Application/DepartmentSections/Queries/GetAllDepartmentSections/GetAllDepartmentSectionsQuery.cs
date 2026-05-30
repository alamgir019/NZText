namespace NZ.HRM.Application.DepartmentSections.Queries.GetAllDepartmentSections;

public class GetAllDepartmentSectionsQuery
{
    public bool IncludeInactive { get; set; } = false;
    public string? DepartmentId { get; set; }
    public string? SectionId { get; set; }
}
