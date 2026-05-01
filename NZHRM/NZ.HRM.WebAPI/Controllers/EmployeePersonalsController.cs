using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.EmployeePersonals.Commands.CreateEmployeePersonal;
using NZ.HRM.Application.EmployeePersonals.Commands.DeleteEmployeePersonal;
using NZ.HRM.Application.EmployeePersonals.Commands.UpdateEmployeePersonal;
using NZ.HRM.Application.EmployeePersonals.Handlers;
using NZ.HRM.Application.EmployeePersonals.Queries.GetAllEmployeePersonals;
using NZ.HRM.Application.EmployeePersonals.Queries.GetEmployeePersonalById;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeePersonalsController : ControllerBase
{
    private readonly EmployeePersonalQueryHandler _queryHandler;
    private readonly EmployeePersonalCommandHandler _commandHandler;

    public EmployeePersonalsController(
        EmployeePersonalQueryHandler queryHandler,
        EmployeePersonalCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    /// <summary>
    /// Get all employee personal information or filter by employee ID
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<EmployeePersonalDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] string? employeeId = null)
    {
        var query = new GetAllEmployeePersonalsQuery { EmployeeId = employeeId };
        var employeePersonals = await _queryHandler.Handle(query);
        return Ok(employeePersonals);
    }

    /// <summary>
    /// Get employee personal information by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EmployeePersonalDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetEmployeePersonalByIdQuery { Id = id };
        var employeePersonal = await _queryHandler.Handle(query);

        if (employeePersonal == null)
            return NotFound(new { message = $"Employee personal information with ID {id} not found" });

        return Ok(employeePersonal);
    }

    /// <summary>
    /// Create employee personal information
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateEmployeePersonalCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var id = await _commandHandler.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Update employee personal information
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateEmployeePersonalCommand command)
    {
        if (id != command.Id)
            return BadRequest(new { message = "ID mismatch" });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _commandHandler.Handle(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Delete employee personal information (soft delete)
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeleteEmployeePersonalCommand { Id = id };

        try
        {
            await _commandHandler.Handle(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
