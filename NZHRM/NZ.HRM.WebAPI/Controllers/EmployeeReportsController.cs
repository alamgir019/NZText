using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Employees.Handlers;
using NZ.HRM.Application.Model.EmployeeReports.DTOs;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeReportsController : ControllerBase
{
    private readonly EmployeeQueryHandler _employeeQueryHandler;

    public EmployeeReportsController(
        EmployeeQueryHandler employeeQueryHandler)
    {
        _employeeQueryHandler = employeeQueryHandler;
    }

    [HttpGet("master-list")]
    [ProducesResponseType(typeof(Application.Model.Employees.DTOs.EmployeeMasterListResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEmployeeMasterList(
        [FromQuery] string? unitId = null,
        [FromQuery] string? subUnitId = null,
        [FromQuery] string? departmentId = null,
        [FromQuery] string? sectionId = null,
        [FromQuery] string? cellId = null,
        [FromQuery] string? employeeNature = null,
        [FromQuery] string? joiningFromDate = null,
        [FromQuery] string? joiningToDate = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] bool includeInactive = false)
    {
        var query = new Application.Employees.Queries.GetEmployeeMasterList.GetEmployeeMasterListQuery
        {
            UnitId = unitId,
            SubUnitId = subUnitId,
            DepartmentId = departmentId,
            SectionId = sectionId,
            CellId = cellId,
            EmployeeNature = employeeNature,
            PageNumber = pageNumber,
            PageSize = pageSize,
            IncludeInactive = includeInactive
        };

        // Parse joining dates if provided
        if (!string.IsNullOrWhiteSpace(joiningFromDate) && DateOnly.TryParse(joiningFromDate, out var fromDate))
        {
            query.JoiningFromDate = fromDate;
        }

        if (!string.IsNullOrWhiteSpace(joiningToDate) && DateOnly.TryParse(joiningToDate, out var toDate))
        {
            query.JoiningToDate = toDate;
        }

        var result = await _employeeQueryHandler.Handle(query, cancellationToken: default);
        return Ok(result);
    }

    [HttpGet("it-activation-summary")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetItActivationSummary([FromQuery] string unitId = "")
    {
        var query = new Application.Employees.Queries.GetItActivationSummary.GetItActivationSummaryQuery { UnitId = unitId, status = "ITActivation", includeInactive = false, date = DateTime.UtcNow };
        var result = await _employeeQueryHandler.Handle(query, cancellationToken: default);
        return Ok(result);
    }

    [HttpGet("medical-report/{employeeId}")]
    [ProducesResponseType(typeof(MedicalReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMedicalReport(string employeeId)
    {
        if (string.IsNullOrWhiteSpace(employeeId))
            return BadRequest(new { message = "employeeId is required" });

        var query = new Application.Employees.Queries.GetMedicalReport.GetMedicalReportQuery { EmployeeId = employeeId };
        var result = await _employeeQueryHandler.Handle(query, cancellationToken: default);

        if (result == null)
            return NotFound(new { message = $"Medical report not found for employee with ID {employeeId}" });

        return Ok(result);
    }

    [HttpGet("candidate-entry/{employeeId}")]
    [ProducesResponseType(typeof(CandidateEntryReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCandidateEntryReport(string employeeId)
    {
        if (string.IsNullOrWhiteSpace(employeeId))
            return BadRequest(new { message = "employeeId is required" });

        var query = new Application.Employees.Queries.GetCandidateEntryReport.GetCandidateEntryReportQuery { EmployeeId = employeeId };
        var result = await _employeeQueryHandler.Handle(query, cancellationToken: default);

        if (result == null)
            return NotFound(new { message = $"Candidate entry information not found for employee with ID {employeeId}" });

        return Ok(result);
    }

}
