namespace NZ.HRM.Application.Departments.Queries.GetDepartmentsByLocation;

public class GetDepartmentsByLocationQuery
{
    public string LocationId { get; set; } = string.Empty;
    public bool IncludeInactive { get; set; } = false;
}
