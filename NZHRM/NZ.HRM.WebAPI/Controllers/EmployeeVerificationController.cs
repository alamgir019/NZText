using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.EmployeeMasters.Handlers;
using NZ.HRM.Application.EmployeeMasters.Queries.VerifyEmployeeCodeUniqueness;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeVerificationController : ControllerBase
{
    private readonly EmployeeMasterQueryHandler _employeeMasterQueryHandler;

    public EmployeeVerificationController(EmployeeMasterQueryHandler employeeMasterQueryHandler)
    {
        _employeeMasterQueryHandler = employeeMasterQueryHandler;
    }

    /// <summary>
    /// Verify if an employee code is unique in the system
    /// </summary>
    /// <param name="employeeCode">The employee code to verify</param>
    /// <returns>Result indicating if the code is unique</returns>
    [HttpGet("verify-code-uniqueness")]
    [ProducesResponseType(typeof(EmployeeCodeUniquenessDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> VerifyEmployeeCodeUniqueness(
        [FromQuery] string employeeCode)
    {
        if (string.IsNullOrWhiteSpace(employeeCode))
        {
            return BadRequest(new { message = "Employee code is required" });
        }

        var query = new VerifyEmployeeCodeUniquenessQuery
        {
            EmployeeCode = employeeCode
        };

        var result = await _employeeMasterQueryHandler.Handle(query, cancellationToken: default);
        return Ok(result);
    }
}
