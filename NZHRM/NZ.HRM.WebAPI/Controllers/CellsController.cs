using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Cells.Commands.CreateCell;
using NZ.HRM.Application.Cells.Commands.DeleteCell;
using NZ.HRM.Application.Cells.Commands.UpdateCell;
using NZ.HRM.Application.Cells.Handlers;
using NZ.HRM.Application.Cells.Queries.GetAllCells;
using NZ.HRM.Application.Cells.Queries.GetCellById;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CellsController : ControllerBase
{
    private readonly CellQueryHandler _queryHandler;
    private readonly CellCommandHandler _commandHandler;

    public CellsController(CellQueryHandler queryHandler, CellCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false, [FromQuery] string? sectionId = null)
    {
        var query = new GetAllCellsQuery { IncludeInactive = includeInactive, SectionId = sectionId };
        var cells = await _queryHandler.Handle(query);
        return Ok(cells);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetCellByIdQuery { Id = id };
        var cell = await _queryHandler.Handle(query);
        if (cell == null) return NotFound(new { message = $"Cell with ID {id} not found" });
        return Ok(cell);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCellCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var id = await _commandHandler.Handle(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateCellCommand command)
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
        var command = new DeleteCellCommand { Id = id };
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
