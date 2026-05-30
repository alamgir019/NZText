namespace NZ.HRM.Application.Grades.Queries.GetAllGrades;

public class GetAllGradesQuery
{
    public bool IncludeInactive { get; set; } = false;
    public string? EmployeeType { get; set; }
}
