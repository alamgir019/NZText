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
    private readonly EmployeeMasterQueryHandler _queryHandler;
    private readonly EmployeeMasterCommandHandler _commandHandler;
    private readonly GetEnrollmentIdQueryHandler _getEnrollmentIdHandler;

    public EmployeeMastersController(
        EmployeeMasterQueryHandler queryHandler,
        EmployeeMasterCommandHandler commandHandler,
        GetEnrollmentIdQueryHandler getEnrollmentIdHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
        _getEnrollmentIdHandler = getEnrollmentIdHandler;
    }

    /// <summary>
    /// Get all employees (can filter by company or department)
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<EmployeeMasterDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] bool includeInactive = false, 
        [FromQuery] string? companyId = null,
        [FromQuery] string? departmentId = null)
    {
        var query = new GetAllEmployeeMastersQuery 
        { 
            IncludeInactive = includeInactive,
            CompanyId = companyId,
            DepartmentId = departmentId
        };
        var employees = await _queryHandler.Handle(query, cancellationToken: default);
        return Ok(employees);
    }

    /// <summary>
    /// Get employee by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EmployeeMasterDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetEmployeeMasterByIdQuery { Id = id };
        var employee = await _queryHandler.Handle(query, cancellationToken: default);

        if (employee == null)
            return NotFound(new { message = $"Employee with ID {id} not found" });

        return Ok(employee);
    }

    /// <summary>
    /// Create a new employee (basic information)
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeMasterCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var employeeId = await _commandHandler.Handle(command, cancellationToken: default);
            return CreatedAtAction(nameof(GetById), new { id = employeeId }, new { id = employeeId });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Update an existing employee
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateEmployeeMasterCommand command)
    {
        if (id != command.Id)
            return BadRequest(new { message = "ID mismatch" });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _commandHandler.Handle(command, cancellationToken: default);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Delete an employee (soft delete)
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeleteEmployeeMasterCommand { Id = id };

        try
        {
            await _commandHandler.Handle(command, cancellationToken: default);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
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
