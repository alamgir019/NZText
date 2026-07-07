using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Districts.Handlers;
using NZ.HRM.Application.Districts.Queries;
using NZ.HRM.Application.Divisions.Handlers;
using NZ.HRM.Application.Divisions.Queries;
using NZ.HRM.Application.Thanas.Handlers;
using NZ.HRM.Application.Thanas.Queries;
using NZ.HRM.Application.Divisions.Commands.CreateDivision;
using NZ.HRM.Application.Divisions.Commands.DeleteDivision;
using NZ.HRM.Application.Divisions.Commands.UpdateDivision;
using NZ.HRM.Application.Divisions.Handlers;
using NZ.HRM.Application.Districts.Commands.CreateDistrict;
using NZ.HRM.Application.Districts.Commands.DeleteDistrict;
using NZ.HRM.Application.Districts.Commands.UpdateDistrict;
using NZ.HRM.Application.Districts.Handlers;
using NZ.HRM.Application.Thanas.Commands.CreateThana;
using NZ.HRM.Application.Thanas.Commands.DeleteThana;
using NZ.HRM.Application.Thanas.Commands.UpdateThana;
using NZ.HRM.Application.Thanas.Handlers;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GeoController : ControllerBase
{
    private readonly DivisionQueryHandler _divisionQueryHandler;
    private readonly DistrictQueryHandler _districtQueryHandler;
    private readonly ThanaQueryHandler _thanaQueryHandler;
    private readonly DivisionCommandHandler _divisionCommandHandler;
    private readonly DistrictCommandHandler _districtCommandHandler;
    private readonly ThanaCommandHandler _thanaCommandHandler;

    public GeoController(
        DivisionQueryHandler divisionQueryHandler,
        DistrictQueryHandler districtQueryHandler,
        ThanaQueryHandler thanaQueryHandler,
        DivisionCommandHandler divisionCommandHandler,
        DistrictCommandHandler districtCommandHandler,
        ThanaCommandHandler thanaCommandHandler)
    {
        _divisionQueryHandler = divisionQueryHandler;
        _districtQueryHandler = districtQueryHandler;
        _thanaQueryHandler = thanaQueryHandler;
        _divisionCommandHandler = divisionCommandHandler;
        _districtCommandHandler = districtCommandHandler;
        _thanaCommandHandler = thanaCommandHandler;
    }

    /// <summary>
    /// Get all divisions
    /// </summary>
    [HttpGet("divisions")]
    [ProducesResponseType(typeof(List<DivisionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllDivisions()
    {
        var divisions = await _divisionQueryHandler.Handle(new GetAllDivisionsQuery());
        return Ok(divisions);
    }

    /// <summary>
    /// Get all districts by division ID
    /// </summary>
    [HttpGet("divisions/{divisionId}/districts")]
    [ProducesResponseType(typeof(List<DistrictDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDistrictsByDivisionId(string divisionId)
    {
        var query = new GetDistrictsByDivisionIdQuery { DivisionId = divisionId };
        var districts = await _districtQueryHandler.Handle(query);

        if (!districts.Any())
            return NotFound(new { message = $"No districts found for division ID {divisionId}" });

        return Ok(districts);
    }

    /// <summary>
    /// Get all thanas by district ID
    /// </summary>
    [HttpGet("districts/{districtId}/thanas")]
    [ProducesResponseType(typeof(List<ThanaDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetThanasByDistrictId(string districtId)
    {
        var query = new GetThanasByDistrictIdQuery { DistrictId = districtId };
        var thanas = await _thanaQueryHandler.Handle(query);

        if (!thanas.Any())
            return NotFound(new { message = $"No thanas found for district ID {districtId}" });

        return Ok(thanas);
    }

    // Division: Create
    [HttpPost("divisions")]
    public async Task<IActionResult> CreateDivision([FromBody] CreateDivisionCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var id = await _divisionCommandHandler.Handle(command);
        return CreatedAtAction(nameof(GetAllDivisions), new { }, new { id });
    }

    // Division: Update
    [HttpPut("divisions/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDivision(string id, [FromBody] UpdateDivisionCommand command)
    {
        if (id != command.Id) return BadRequest(new { message = "ID mismatch" });
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _divisionCommandHandler.Handle(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // Division: Delete (soft)
    [HttpDelete("divisions/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDivision(string id)
    {
        var command = new DeleteDivisionCommand { Id = id };
        try
        {
            await _divisionCommandHandler.Handle(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // District: Create
    [HttpPost("divisions/{divisionId}/districts")]
    [ProducesResponseType(typeof(DistrictDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateDistrict(string divisionId, [FromBody] CreateDistrictCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // ensure route divisionId is applied
        command.DivisionId = divisionId;

        try
        {
            var id = await _districtCommandHandler.Handle(command);
            return CreatedAtAction(nameof(GetDistrictsByDivisionId), new { divisionId = divisionId }, new { id });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // District: Update
    [HttpPut("districts/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDistrict(string id, [FromBody] UpdateDistrictCommand command)
    {
        if (id != command.Id) return BadRequest(new { message = "ID mismatch" });
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _districtCommandHandler.Handle(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // District: Delete (soft)
    [HttpDelete("districts/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDistrict(string id)
    {
        var command = new DeleteDistrictCommand { Id = id };
        try
        {
            await _districtCommandHandler.Handle(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // Thana: Create
    [HttpPost("districts/{districtId}/thanas")]
    [ProducesResponseType(typeof(ThanaDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateThana(string districtId, [FromBody] CreateThanaCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        command.DistrictId = districtId;

        try
        {
            var id = await _thanaCommandHandler.Handle(command);
            return CreatedAtAction(nameof(GetThanasByDistrictId), new { districtId = districtId }, new { id });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // Thana: Update
    [HttpPut("thanas/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateThana(string id, [FromBody] UpdateThanaCommand command)
    {
        if (id != command.Id) return BadRequest(new { message = "ID mismatch" });
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _thanaCommandHandler.Handle(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // Thana: Delete (soft)
    [HttpDelete("thanas/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteThana(string id)
    {
        var command = new DeleteThanaCommand { Id = id };
        try
        {
            await _thanaCommandHandler.Handle(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
