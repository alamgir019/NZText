using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.GroupComplexes.Commands.CreateGroupComplex;
using NZ.HRM.Application.GroupComplexes.Commands.DeleteGroupComplex;
using NZ.HRM.Application.GroupComplexes.Commands.UpdateGroupComplex;
using NZ.HRM.Application.GroupComplexes.Handlers;
using NZ.HRM.Application.GroupComplexes.Queries.GetAllGroupComplexes;
using NZ.HRM.Application.GroupComplexes.Queries.GetGroupComplexById;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupComplexesController : ControllerBase
{
    private readonly GroupComplexQueryHandler _queryHandler;
    private readonly GroupComplexCommandHandler _commandHandler;

    public GroupComplexesController(GroupComplexQueryHandler queryHandler, GroupComplexCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
    {
        var query = new GetAllGroupComplexesQuery { IncludeInactive = includeInactive };
        var items = await _queryHandler.Handle(query);
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetGroupComplexByIdQuery { Id = id };
        var item = await _queryHandler.Handle(query);
        if (item == null) return NotFound(new { message = $"GroupComplex with ID {id} not found" });
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGroupComplexCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var id = await _commandHandler.Handle(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateGroupComplexCommand command)
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
        var command = new DeleteGroupComplexCommand { Id = id };
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
