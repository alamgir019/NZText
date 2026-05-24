using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Shifts.Commands.CreateShift;
using NZ.HRM.Application.Shifts.Commands.DeleteShift;
using NZ.HRM.Application.Shifts.Commands.UpdateShift;
using NZ.HRM.Application.Shifts.Handlers;
using NZ.HRM.Application.Shifts.Queries.GetAllShifts;
using NZ.HRM.Application.Shifts.Queries.GetShiftById;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftsController : ControllerBase
{
    private readonly ShiftQueryHandler _queryHandler;
    private readonly ShiftCommandHandler _commandHandler;

    public ShiftsController(ShiftQueryHandler queryHandler, ShiftCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ShiftDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
    {
        var query = new GetAllShiftsQuery { IncludeInactive = includeInactive };
        var items = await _queryHandler.Handle(query);
        return Ok(items);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ShiftDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetShiftByIdQuery { Id = id };
        var item = await _queryHandler.Handle(query);
        if (item == null) return NotFound(new { message = $"Shift with ID {id} not found" });
        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateShiftCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var id = await _commandHandler.Handle(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateShiftCommand command)
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeleteShiftCommand { Id = id };
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
