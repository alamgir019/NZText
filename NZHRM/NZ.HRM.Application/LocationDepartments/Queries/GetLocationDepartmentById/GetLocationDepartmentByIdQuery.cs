namespace NZ.HRM.Application.LocationDepartments.Queries.GetLocationDepartmentById;

public class GetLocationDepartmentByIdQuery
{
    public string Id { get; set; } = string.Empty;
}

public class LocationDepartmentDetailDto
{
    public string Id { get; set; } = string.Empty;
    public string ComplexId { get; set; } = string.Empty;
    public string ComplexName { get; set; } = string.Empty;
    public string UnitId { get; set; } = string.Empty;
    public string UnitName { get; set; } = string.Empty;
    public string DepartmentId { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
