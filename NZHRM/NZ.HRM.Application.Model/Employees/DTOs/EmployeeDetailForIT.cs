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
    public decimal? ProbationPeriod { get; set; }
    public string? ReportingTo { get; set; }
    public EmployeeDocumentDto[]? Documents { get; set; }
    public string? FatherNameBangla { get; set; }
    public string? MotherName { get; set; }
    public string? MotherNameBangla { get; set; }
    public string? UnitId { get; set; }
    public string? SubUnitId { get; set; }
    public string? DepartmentId { get; set; }
    public string? SectionId { get; set; }
    public string? CellId { get; set; }
    public string? GradeId { get; set; }
    public string? DesignationId { get; set; }
    public string? ShiftId { get; set; }
}
