using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Companies.Commands.CreateUnit;
using NZ.HRM.Application.Companies.Commands.DeleteUnit;
using NZ.HRM.Application.Companies.Commands.UpdateUnit;
using NZ.HRM.Application.Companies.Handlers;
using NZ.HRM.Application.Units.Handlers;
using NZ.HRM.Application.Units.Queries.GetAllUnits;
using NZ.HRM.Application.Units.Queries.GetUnitById;

namespace NZ.HRM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UnitController : ControllerBase
{
    private readonly UnitsQueryHandler _unitsQueryHandler;
    private readonly UnitsCommandHandler _unitsCommandHandler;

    public UnitController(
        UnitsQueryHandler unitsQueryHandler,
        UnitsCommandHandler unitsCommandHandler)
    {
        _unitsQueryHandler = unitsQueryHandler;
        _unitsCommandHandler = unitsCommandHandler;
    }

    /// <summary>
    /// Get all units
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<UnitDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
    {
        var query = new GetAllUnitsQuery { IncludeInactive = includeInactive };
        var units = await _unitsQueryHandler.Handle(query);
        return Ok(units);
    }

    /// <summary>
    /// Get unit by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UnitDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetUnitByIdQuery { Id = id };
        var unit = await _unitsQueryHandler.Handle(query);

        if (unit == null)
            return NotFound(new { message = $"Unit with ID {id} not found" });

        return Ok(unit);
    }

    /// <summary>
    /// Create a new unit
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUnitCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var unitId = await _unitsCommandHandler.Handle(command);
        return CreatedAtAction(nameof(GetById), new { id = unitId }, new { id = unitId });
    }

    /// <summary>
    /// Update an existing unit
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateUnitCommand command)
    {
        if (id != command.Id)
            return BadRequest(new { message = "ID mismatch" });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _unitsCommandHandler.Handle(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Delete a unit (soft delete)
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeleteUnitCommand { Id = id };

        try
        {
            await _unitsCommandHandler.Handle(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}