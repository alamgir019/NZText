using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.LocationDepartments.Commands.CreateLocationDepartment;
using NZ.HRM.Application.LocationDepartments.Commands.DeleteLocationDepartment;
using NZ.HRM.Application.LocationDepartments.Commands.UpdateLocationDepartment;
using NZ.HRM.Application.LocationDepartments.Handlers;
using NZ.HRM.Application.LocationDepartments.Queries.GetAllLocationDepartments;
using NZ.HRM.Application.LocationDepartments.Queries.GetLocationDepartmentById;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComplexUnitDepartmentsController : ControllerBase
{
    private readonly LocationDepartmentQueryHandler _queryHandler;
    private readonly LocationDepartmentCommandHandler _commandHandler;

    public ComplexUnitDepartmentsController(
        LocationDepartmentQueryHandler queryHandler,
        LocationDepartmentCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<LocationDepartmentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] bool includeInactive = false,
        [FromQuery] string? locationId = null,
        [FromQuery] string? departmentId = null)
    {
        var query = new GetAllLocationDepartmentsQuery
        {
            IncludeInactive = includeInactive,
            LocationId = locationId,
            DepartmentId = departmentId
        };

        var items = await _queryHandler.Handle(query);
        return Ok(items);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LocationDepartmentDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetLocationDepartmentByIdQuery { Id = id };
        var item = await _queryHandler.Handle(query);

        if (item == null)
            return NotFound(new { message = $"LocationDepartment with ID {id} not found" });

        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateLocationDepartmentCommand command)
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
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateLocationDepartmentCommand command)
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

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeleteLocationDepartmentCommand { Id = id };

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
