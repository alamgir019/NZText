namespace NZ.HRM.Application.Model.LearnerAdjustments.DTOs;

public class EligibleLearnerDto
{
    public string EmployeeId { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public DateOnly DateOfJoining { get; set; }
    public DateOnly ProbationCompletedOn { get; set; }
    public decimal CurrentGrossSalary { get; set; }
    public decimal StandardGrossSalary { get; set; }
    public decimal AdjustmentAmount { get; set; }
}
