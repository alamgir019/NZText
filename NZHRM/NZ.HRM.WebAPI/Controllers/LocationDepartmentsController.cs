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
public class LocationDepartmentsController : ControllerBase
{
    private readonly LocationDepartmentQueryHandler _queryHandler;
    private readonly LocationDepartmentCommandHandler _commandHandler;

    public LocationDepartmentsController(LocationDepartmentQueryHandler queryHandler, LocationDepartmentCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false, [FromQuery] string? complexId = null, [FromQuery] string? unitId = null)
    {
        var query = new GetAllLocationDepartmentsQuery { IncludeInactive = includeInactive, ComplexId = complexId, UnitId = unitId };
        var items = await _queryHandler.Handle(query);
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetLocationDepartmentByIdQuery { Id = id };
        var item = await _queryHandler.Handle(query);
        if (item == null) return NotFound(new { message = $"Mapping with ID {id} not found" });
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLocationDepartmentCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var id = await _commandHandler.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateLocationDepartmentCommand command)
    {
        if (id != command.Id) return BadRequest(new { message = "ID mismatch" });
        if (!ModelState.IsValid) return BadRequest(ModelState);
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
