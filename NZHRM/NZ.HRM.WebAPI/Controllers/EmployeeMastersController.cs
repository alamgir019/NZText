using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.EmployeeMasters.Commands.CreateEmployeeMaster;
using NZ.HRM.Application.EmployeeMasters.Commands.DeleteEmployeeMaster;
using NZ.HRM.Application.EmployeeMasters.Commands.UpdateEmployeeMaster;
using NZ.HRM.Application.EmployeeMasters.Handlers;
using NZ.HRM.Application.EmployeeMasters.Queries.GetAllEmployeeMasters;
using NZ.HRM.Application.EmployeeMasters.Queries.GetEmployeeMasterById;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeMastersController : ControllerBase
{
    private readonly GetEnrollmentIdQueryHandler _getEnrollmentIdHandler;

    public EmployeeMastersController(
        GetEnrollmentIdQueryHandler getEnrollmentIdHandler)
    {
        _getEnrollmentIdHandler = getEnrollmentIdHandler;
    }

    /// <summary>
    /// Generate a new enrollment id in format {ddMMyy}{NNN}
    /// </summary>
    [HttpGet("enrollment-id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEnrollmentId()
    {
        var query = new Application.EmployeeMasters.Queries.GetEnrollmentId.GetEnrollmentIdQuery
        {
            Today = DateTime.UtcNow
        };
        var enrollmentId = await _getEnrollmentIdHandler.Handle(query, cancellationToken: default);
        return Ok(new { enrollmentId });
    }
}
