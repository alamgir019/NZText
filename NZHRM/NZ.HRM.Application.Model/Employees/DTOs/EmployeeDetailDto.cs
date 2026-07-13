using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Model.Employees.DTOs;

public class EmployeeDetailDto
{
    public string Id { get; set; } = string.Empty;
    public string EnrollmentId { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;
    public string EmployeeNameEnglish { get; set; } = string.Empty;
    public string? EmployeeNameBangla { get; set; }
    public string? EmployeeName { get; set; } = string.Empty;
    public string UnitName { get; set; } = string.Empty;
    public string SubUnitName { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public string SectionName { get; set; } = string.Empty;
    public string CellName { get; set; } = string.Empty;
    public string GradeName { get; set; } = string.Empty;
    public string DesignationName { get; set; } = string.Empty;
    public string ShiftName { get; set; } = string.Empty;
    public decimal? ProposedMonthlySalary { get; set; }
    public Gender? Gender { get; set; }
    public BloodGroup? BloodGroup { get; set; }
    public DateOnly? JoiningDate { get; set; }
    public EmployeeNature? EmployeeType { get; set; }
}
