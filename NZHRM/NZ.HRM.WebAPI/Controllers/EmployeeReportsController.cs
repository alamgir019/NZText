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
