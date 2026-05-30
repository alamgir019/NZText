using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.SectionCells.Commands.CreateSectionCell;
using NZ.HRM.Application.SectionCells.Commands.DeleteSectionCell;
using NZ.HRM.Application.SectionCells.Commands.UpdateSectionCell;
using NZ.HRM.Application.SectionCells.Handlers;
using NZ.HRM.Application.SectionCells.Queries.GetAllSectionCells;
using NZ.HRM.Application.SectionCells.Queries.GetSectionCellById;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SectionCellsController : ControllerBase
{
    private readonly SectionCellQueryHandler _queryHandler;
    private readonly SectionCellCommandHandler _commandHandler;

    public SectionCellsController(
        SectionCellQueryHandler queryHandler,
        SectionCellCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<SectionCellDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] bool includeInactive = false,
        [FromQuery] string? sectionId = null,
        [FromQuery] string? cellId = null)
    {
        var query = new GetAllSectionCellsQuery
        {
            IncludeInactive = includeInactive,
            SectionId = sectionId,
            CellId = cellId
        };

        var items = await _queryHandler.Handle(query);
        return Ok(items);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SectionCellDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetSectionCellByIdQuery { Id = id };
        var item = await _queryHandler.Handle(query);

        if (item == null)
            return NotFound(new { message = $"SectionCell with ID {id} not found" });

        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateSectionCellCommand command)
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
    public async Task<IActionResult> Update(string id, [FromBody] UpdateSectionCellCommand command)
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
        var command = new DeleteSectionCellCommand { Id = id };

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
