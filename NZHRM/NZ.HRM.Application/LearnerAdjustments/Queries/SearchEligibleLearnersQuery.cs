namespace NZ.HRM.Application.LearnerAdjustments.Queries;

public class SearchEligibleLearnersQuery
{
    public DateOnly? JoiningDateFrom { get; set; }
    public DateOnly? JoiningDateTo { get; set; }
    public int ProbationPeriodMonths { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
