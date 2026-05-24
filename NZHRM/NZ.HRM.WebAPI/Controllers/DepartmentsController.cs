using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Departments.Commands.CreateDepartment;
using NZ.HRM.Application.Departments.Commands.DeleteDepartment;
using NZ.HRM.Application.Departments.Commands.UpdateDepartment;
using NZ.HRM.Application.Departments.Handlers;
using NZ.HRM.Application.Departments.Queries.GetAllDepartments;
using NZ.HRM.Application.Departments.Queries.GetDepartmentsByLocation;
using NZ.HRM.Application.Departments.Queries.GetDepartmentById;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly DepartmentQueryHandler _queryHandler;
    private readonly DepartmentCommandHandler _commandHandler;

    public DepartmentsController(
        DepartmentQueryHandler queryHandler,
        DepartmentCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    /// <summary>
    /// Get all departments
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<DepartmentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
    {
        var query = new GetAllDepartmentsQuery { IncludeInactive = includeInactive };
        var departments = await _queryHandler.Handle(query);
        return Ok(departments);
    }

    /// <summary>
    /// Get departments by location with Head Office specific rules
    /// </summary>
    [HttpGet("by-location/{locationId}")]
    [ProducesResponseType(typeof(List<DepartmentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByLocation(string locationId, [FromQuery] bool includeInactive = false)
    {
        var query = new GetDepartmentsByLocationQuery
        {
            LocationId = locationId,
            IncludeInactive = includeInactive
        };

        var departments = await _queryHandler.Handle(query);
        return Ok(departments);
    }

    /// <summary>
    /// Get department by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DepartmentDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetDepartmentByIdQuery { Id = id };
        var department = await _queryHandler.Handle(query);

        if (department == null)
            return NotFound(new { message = $"Department with ID {id} not found" });

        return Ok(department);
    }

    /// <summary>
    /// Create a new department
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var departmentId = await _commandHandler.Handle(command);
        return CreatedAtAction(nameof(GetById), new { id = departmentId }, new { id = departmentId });
    }

    /// <summary>
    /// Update an existing department
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateDepartmentCommand command)
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
    /// Delete a department (soft delete)
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeleteDepartmentCommand { Id = id };

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
