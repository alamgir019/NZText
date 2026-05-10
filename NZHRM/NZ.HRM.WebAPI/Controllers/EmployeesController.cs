using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Employees.Commands.CreateCompleteEmployee;
using NZ.HRM.Application.Employees.DTOs;
using NZ.HRM.Application.Employees.Handlers;
using NZ.HRM.Application.Employees.Queries.GetCompleteEmployee;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly CreateCompleteEmployeeCommandHandler _createCompleteEmployeeHandler;
    private readonly GetCompleteEmployeeQueryHandler _getCompleteEmployeeHandler;

    public EmployeesController(
        CreateCompleteEmployeeCommandHandler createCompleteEmployeeHandler,
        GetCompleteEmployeeQueryHandler getCompleteEmployeeHandler)
    {
        _createCompleteEmployeeHandler = createCompleteEmployeeHandler;
        _getCompleteEmployeeHandler = getCompleteEmployeeHandler;
    }

    /// <summary>
    /// Create a complete employee record (both master and personal information)
    /// </summary>
    /// <remarks>
    /// This endpoint creates both EmployeeMaster and EmployeePersonal records in a single transaction.
    /// 
    /// Sample request:
    /// 
    ///     POST /api/employees
    ///     {
    ///         "employeeCode": "EMP-2026-0415",
    ///         "employeeNameEnglish": "Rahim Uddin",
    ///         "employeeNameBangla": "???? ??????",
    ///         "companyId": "01HQZXY00000000000000001",
    ///         "departmentId": "01HQZXY00000000000000002",
    ///         "sectionId": "01HQZXY00000000000000003",
    ///         "gradeId": "01HQZXY00000000000000004",
    ///         "employeeType": 0,
    ///         "shiftId": "01HQZXY00000000000000005",
    ///         "employeeNature": 0,
    ///         "joiningDate": "2026-04-15",
    ///         "dateOfBirth": "2002-01-01",
    ///         "gender": 0,
    ///         "maritalStatus": 0,
    ///         "mobileNumber": "01712-345678",
    ///         "documentType": 0,
    ///         "documentNumber": "19876543210987654",
    ///         "religion": 0,
    ///         "nationality": 0,
    ///         "fatherNameEnglish": "Abdul Karim",
    ///         "motherNameEnglish": "Fatema Begum"
    ///     }
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCompleteEmployee([FromBody] CreateCompleteEmployeeCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var employeeId = await _createCompleteEmployeeHandler.Handle(command, cancellationToken: default);
            return CreatedAtAction(
                nameof(GetCompleteEmployee), 
                new { id = employeeId }, 
                new { id = employeeId, message = "Employee created successfully with personal information" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the employee", details = ex.Message });
        }
    }

    /// <summary>
    /// Get complete employee information (master + personal)
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EmployeeCompleteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCompleteEmployee(string id)
    {
        var query = new GetCompleteEmployeeQuery { EmployeeId = id };
        var employee = await _getCompleteEmployeeHandler.Handle(query, cancellationToken: default);

        if (employee == null)
            return NotFound(new { message = $"Employee with ID {id} not found" });

        return Ok(employee);
    }
}
