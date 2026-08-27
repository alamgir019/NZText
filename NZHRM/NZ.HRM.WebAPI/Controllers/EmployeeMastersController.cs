using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.EmployeeMasters.Handlers;
using NZ.HRM.Application.Employees.Handlers;
using NZ.HRM.Application.Employees.Queries.GetEmployeeBasicInformation;
using NZ.HRM.Application.Model.Employees.DTOs;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeMastersController : ControllerBase
{
    private readonly GetEnrollmentIdQueryHandler _getEnrollmentIdHandler;
	private readonly GetEmployeeBasicInformationQueryHandler _basicEmployeeInformationHandler;

	public EmployeeMastersController(
        GetEnrollmentIdQueryHandler getEnrollmentIdHandler,
        GetEmployeeBasicInformationQueryHandler basicEmployeeInformationHandler)
    {
        _getEnrollmentIdHandler = getEnrollmentIdHandler;
        _basicEmployeeInformationHandler = basicEmployeeInformationHandler;
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

	[HttpGet("basic-information")]
	[ProducesResponseType(typeof(BasicEmployeeInformationDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetBasicEmployeeInformation(string searchText)
	{
		if (string.IsNullOrWhiteSpace(searchText))
			return BadRequest(new { message = "employeeCode is required" });

		var query = new GetEmployeeBasicInformationQuery
		{
			SearchText = searchText
		};

		var result = await _basicEmployeeInformationHandler.Handle(query, HttpContext.RequestAborted);

		if (result == null)
			return NotFound(new { message = $"Employee with code {searchText} not found" });

		return Ok(result);
	}
}
