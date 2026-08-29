namespace NZ.HRM.Application.Model.Employees.DTOs;

public class EmployeeBasicInfoDto
{
    public string Id { get; set; } = string.Empty;
    public string? EnrollmentId { get; set; }
    public string EmployeeNameEnglish { get; set; } = string.Empty;
    public string? MobileNumber { get; set; }
    public string? EmployeeNameBangla { get; set; }
    public string? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public string? DesignationId { get; set; }
    public string? EmployeeCode { get; set; }
    public string? DesignationName { get; set; }



}
