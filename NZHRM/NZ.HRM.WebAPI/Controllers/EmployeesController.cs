using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Employees.Handlers;
using NZ.HRM.Application.Employees.Queries.GetCompleteEmployee;
using NZ.HRM.Application.Employees.Queries.GetEmployeeConfirmationDate;
using NZ.HRM.Application.Employees.Queries.GetEmployeesByStatus;
using NZ.HRM.Application.Employees.Queries.SearchEmployees;
using NZ.HRM.Application.Model.Employees.Commands.CreateCompleteEmployee;
using NZ.HRM.Application.Model.Employees.DTOs;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly CompleteEmployeeCommandHandler _createCompleteEmployeeHandler;
    private readonly CompleteEmployeeQueryHandler _getCompleteEmployeeHandler;

    public EmployeesController(
        CompleteEmployeeCommandHandler createCompleteEmployeeHandler,
        CompleteEmployeeQueryHandler getCompleteEmployeeHandler)
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
                new { id = employeeId, message = "Personal Information Saved Successfully" });
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

    [HttpPost("candidate-entry")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateEmployeeRecruitment([FromBody] CreateCandidateEntryCommand command)
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

    [HttpGet("search")]
    [ProducesResponseType(typeof(List<EmployeeCompleteDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchEmployees([FromQuery] string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
            return BadRequest(new { message = "searchText is required" });

        var query = new SearchEmployeesQuery { SearchText = searchText };
        var employees = await _getCompleteEmployeeHandler.Handle(query, cancellationToken: default);

        return Ok(employees);
    }

    [HttpGet("confirmation-date")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetConfirmationDate([FromQuery] string employeeId, [FromQuery] DateTime joiningDate)
    {
        var query = new GetEmployeeConfirmationDateQuery
        {
            EmployeeId = employeeId,
            JoiningDate = joiningDate
        };

        var confirmationDate = await _getCompleteEmployeeHandler.Handle(query, cancellationToken: default);
        if (confirmationDate == null)
            return NotFound(new { message = $"Employee with ID {employeeId} not found" });

        return Ok(new { confirmationDate = confirmationDate.Value.ToString("yyyy-MM-dd") });
    }

    [HttpGet("by-status")]
    [ProducesResponseType(typeof(List<EmployeeByStatusDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetEmployeesByStatus([FromQuery] string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            return BadRequest(new { message = "status is required" });

        var query = new GetEmployeesByStatusQuery
        {
            Status = status
        };

        var employees = await _getCompleteEmployeeHandler.Handle(query, cancellationToken: default);
        return Ok(employees);
    }

    
}
