namespace NZ.HRM.Application.LocationDepartments.Queries.GetAllLocationDepartments;

public class GetAllLocationDepartmentsQuery
{
    public bool IncludeInactive { get; set; } = false;
    public string? LocationId { get; set; }
    public string? DepartmentId { get; set; }
}
