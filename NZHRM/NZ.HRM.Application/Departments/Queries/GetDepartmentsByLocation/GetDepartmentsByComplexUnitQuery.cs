namespace NZ.HRM.Application.Departments.Queries.GetDepartmentsByLocation;

public class GetDepartmentsByComplexUnitQuery
{
    public string ComplexId { get; set; } = string.Empty;
    public string UnitId { get; set; } = string.Empty;
    public bool IncludeInactive { get; set; } = false;
}
