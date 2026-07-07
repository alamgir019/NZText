namespace NZ.HRM.Application.Employees.Queries.GetItActivationSummary;

public class GetItActivationSummaryQuery
{
    public string status;
    public bool includeInactive;
    public DateTime date;

    public string UnitId { get; set; }
}
