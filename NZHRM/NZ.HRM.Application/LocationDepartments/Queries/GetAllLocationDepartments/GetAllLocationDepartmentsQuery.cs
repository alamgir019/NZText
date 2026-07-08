namespace NZ.HRM.Application.LocationDepartments.Queries.GetAllLocationDepartments;

public class GetAllLocationDepartmentsQuery
{
    public bool IncludeInactive { get; set; }
    public string? ComplexId { get; set; }
    public string? UnitId { get; set; }
}

public class LocationDepartmentDto
{
    public string Id { get; set; } = string.Empty;
    public string ComplexId { get; set; } = string.Empty;
    public string ComplexName { get; set; } = string.Empty;
    public string UnitId { get; set; } = string.Empty;
    public string UnitName { get; set; } = string.Empty;
    public string DepartmentId { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
}
