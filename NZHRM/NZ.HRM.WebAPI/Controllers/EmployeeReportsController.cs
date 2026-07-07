using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.EmployeeMasters.Handlers;
using NZ.HRM.Application.Employees.Handlers;
using NZ.HRM.Application.Employees.Queries.GetEmployeeConfirmationDate;
using NZ.HRM.Application.Employees.Queries.GetEmployeeDetail;
using NZ.HRM.Application.Employees.Queries.GetEmployeesByStatus;
using NZ.HRM.Application.Employees.Queries.SearchEmployees;
using NZ.HRM.Application.Model.Employees.Commands.CreateCompleteEmployee;
using NZ.HRM.Application.Model.Employees.DTOs;

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
}
