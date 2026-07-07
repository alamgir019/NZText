namespace NZ.HRM.Application.Employees.Queries.GetItActivationSummary;

public class ItActivationSummaryDto
{
    public int Total { get; set; }
    public int Workers { get; set; }
    public int Staff { get; set; }
    public int Management { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public List<ItActivationSummaryDto> CompanySummaries { get; set; } = new List<ItActivationSummaryDto>();
}
