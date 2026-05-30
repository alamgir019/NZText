namespace NZ.HRM.Application.FinancialDetails.Queries.GetAllFinancialDetails;

public class GetAllFinancialDetailsQuery
{
    public bool IncludeInactive { get; set; }
    public string? EmployeeId { get; set; }
}
