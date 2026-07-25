using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Grades.Queries.GetGradeById;

public class GradeDetailDto
{
    public string Id { get; set; } = string.Empty;
    public string GradeName { get; set; } = string.Empty;
    public decimal MinSalary { get; set; }
    public decimal MaxSalary { get; set; }
    public EmployeeNature? EmployeeNature { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime UpdatedOn { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
