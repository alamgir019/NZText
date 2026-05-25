namespace NZ.HRM.Application.EmployeeNatures.Queries.GetAllEmployeeNatures;

public class EmployeeNatureDto
{
    public string Id { get; set; } = string.Empty;
    public string NatureName { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime UpdatedOn { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
