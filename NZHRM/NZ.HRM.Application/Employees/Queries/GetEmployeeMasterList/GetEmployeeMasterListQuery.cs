using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Employees.Queries.GetEmployeeMasterList;

public class GetEmployeeMasterListQuery
{
    /// <summary>
    /// Unit ID filter. If not provided, includes all units.
    /// </summary>
    public string? UnitId { get; set; }

    /// <summary>
    /// SubUnit ID filter. If not provided, includes all sub-units.
    /// </summary>
    public string? SubUnitId { get; set; }

    /// <summary>
    /// Department ID filter. If not provided, includes all departments.
    /// </summary>
    public string? DepartmentId { get; set; }

    /// <summary>
    /// Section ID filter. If not provided, includes all sections.
    /// </summary>
    public string? SectionId { get; set; }

    /// <summary>
    /// Cell ID filter. If not provided, includes all cells.
    /// </summary>
    public string? CellId { get; set; }

    /// <summary>
    /// Employee Nature filter (e.g., Worker, Staff, Management). If not provided, includes all natures.
    /// </summary>
    public string? EmployeeNature { get; set; }

    /// <summary>
    /// Joining date from filter (inclusive). If not provided, no lower bound.
    /// </summary>
    public DateOnly? JoiningFromDate { get; set; }

    /// <summary>
    /// Joining date to filter (inclusive). If not provided, no upper bound.
    /// </summary>
    public DateOnly? JoiningToDate { get; set; }

    /// <summary>
    /// Page number (1-based). Default is 1.
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Page size. Default is 20.
    /// </summary>
    public int PageSize { get; set; } = 20;

    /// <summary>
    /// Include inactive employees. Default is false.
    /// </summary>
    public bool IncludeInactive { get; set; } = false;
    public Religion? Religion { get; set; }
    public string? EmployeeCode { get; set; }
    public string? EmployeeMobile { get; set; }
    public Gender? Gender { get; set; }
    public string? GradeId { get; set; }
    public string? ShiftId { get; set; }
    public string? DivisionId { get; set; }
    public string? IdNumber { get; set; }
}
