using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Model.Employees.DTOs;

public class EmployeeByStatusDto
{
    public string EmployeeId { get; set; } = string.Empty;
    public string EnrollmentId { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public int? Age { get; set; }
    public DateTime? ExaminationDate { get; set; }
    public DateOnly? DateOfJoining { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public BloodGroup? BloodGroup { get; set; }
    public string? Department { get; set; }
    public string? Section { get; set; }
    public string? Cell { get; set; }
    public string? Designation { get; set; }
    public string? Grade { get; set; }
    public string? Shift { get; set; }
    public WeekOffDay? WeekOffDay { get; set; }
    public decimal? ProposedSalary { get; set; }
}
