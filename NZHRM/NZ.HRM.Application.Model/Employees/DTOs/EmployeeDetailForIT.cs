using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Model.Employees.DTOs;

public class EmployeeDetailForIT : EmployeeDetailDto
{
    public string? FatherName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? Religion { get; set; }
    public string? NomineeName { get; set; }
    public string? NomineeRelation { get; set; }
    public string? Mobile { get; set; } = null;
    public bool? ApprovedByDirector { get; set; }
    public string? Department { get; set; }
    public string? WeekOffDay { get; set; }
    public string? PayBasis { get; set; }
    public string? ProbationPeriod { get; set; }
    public string? ReportingTo { get; set; }
    public EmployeeDocumentDto[]? Documents { get; set; }
}
