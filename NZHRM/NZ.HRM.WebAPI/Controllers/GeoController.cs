using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Districts.Handlers;
using NZ.HRM.Application.Districts.Queries;
using NZ.HRM.Application.Divisions.Handlers;
using NZ.HRM.Application.Divisions.Queries;
using NZ.HRM.Application.Thanas.Handlers;
using NZ.HRM.Application.Thanas.Queries;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GeoController : ControllerBase
{
    private readonly DivisionQueryHandler _divisionQueryHandler;
    private readonly DistrictQueryHandler _districtQueryHandler;
    private readonly ThanaQueryHandler _thanaQueryHandler;

    public GeoController(
        DivisionQueryHandler divisionQueryHandler,
        DistrictQueryHandler districtQueryHandler,
        ThanaQueryHandler thanaQueryHandler)
    {
        _divisionQueryHandler = divisionQueryHandler;
        _districtQueryHandler = districtQueryHandler;
        _thanaQueryHandler = thanaQueryHandler;
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
}
