using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Designations.Commands.CreateDesignation;
using NZ.HRM.Application.Designations.Commands.DeleteDesignation;
using NZ.HRM.Application.Designations.Commands.UpdateDesignation;
using NZ.HRM.Application.Designations.Handlers;
using NZ.HRM.Application.Designations.Queries.GetAllDesignations;
using NZ.HRM.Application.Designations.Queries.GetDesignationById;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DesignationsController : ControllerBase
{
    private readonly DesignationQueryHandler _queryHandler;
    private readonly DesignationCommandHandler _commandHandler;

    public DesignationsController(DesignationQueryHandler queryHandler, DesignationCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<DesignationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
    {
        var query = new GetAllDesignationsQuery { IncludeInactive = includeInactive };
        var items = await _queryHandler.Handle(query);
        return Ok(items);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DesignationDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetDesignationByIdQuery { Id = id };
        var item = await _queryHandler.Handle(query);
        if (item == null) return NotFound(new { message = $"Designation with ID {id} not found" });
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDesignationCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var id = await _commandHandler.Handle(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateDesignationCommand command)
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
        var command = new DeleteDesignationCommand { Id = id };
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
