namespace NZ.HRM.Application.Model.Employees.DTOs;

public class EmployeeSearchDto
{
    public string Id { get; set; } = string.Empty;
    public string? EnrollmentId { get; set; }
    public string EmployeeNameEnglish { get; set; } = string.Empty;
    public string? MobileNumber { get; set; }
    public string? EmployeeNameBangla { get; set; }
}
