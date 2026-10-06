namespace NZ.HRM.Application.Employees.Queries.GetProbationCompletion;

public class GetProbationCompletionQuery
{
    public string? DepartmentId { get; set; }
    public string? SectionId { get; set; }
    public DateOnly? ProbationEndDate { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
