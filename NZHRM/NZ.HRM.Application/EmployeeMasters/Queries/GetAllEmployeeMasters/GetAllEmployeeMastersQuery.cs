namespace NZ.HRM.Application.EmployeeMasters.Queries.GetAllEmployeeMasters;

public class GetAllEmployeeMastersQuery
{
    public bool IncludeInactive { get; set; } = false;
    public string? CompanyId { get; set; }
    public string? DepartmentId { get; set; }
}
