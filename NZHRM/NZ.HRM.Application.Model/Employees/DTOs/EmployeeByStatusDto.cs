namespace NZ.HRM.Application.Model.Employees.DTOs;

public class EmployeeByStatusDto
{
    public string EmployeeId { get; set; } = string.Empty;
    public string EnrollmentId { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public int? Age { get; set; }
    public DateTime? ExaminationDate { get; set; }
}
