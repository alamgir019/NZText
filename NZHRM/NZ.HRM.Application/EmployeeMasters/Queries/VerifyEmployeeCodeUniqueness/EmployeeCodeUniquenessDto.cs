namespace NZ.HRM.Application.EmployeeMasters.Queries.VerifyEmployeeCodeUniqueness;

public class EmployeeCodeUniquenessDto
{
    public bool IsUnique { get; set; }
    public string? Message { get; set; }
}
