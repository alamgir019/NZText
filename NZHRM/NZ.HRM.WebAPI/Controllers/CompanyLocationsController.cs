using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.CompanyLocations.Commands.CreateCompanyLocation;
using NZ.HRM.Application.CompanyLocations.Commands.DeleteCompanyLocation;
using NZ.HRM.Application.CompanyLocations.Commands.UpdateCompanyLocation;
using NZ.HRM.Application.CompanyLocations.Handlers;
using NZ.HRM.Application.CompanyLocations.Queries.GetAllCompanyLocations;
using NZ.HRM.Application.CompanyLocations.Queries.GetCompanyLocationById;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyLocationsController : ControllerBase
{
    private readonly CompanyLocationQueryHandler _queryHandler;
    private readonly CompanyLocationCommandHandler _commandHandler;

    public CompanyLocationsController(
        CompanyLocationQueryHandler queryHandler,
        CompanyLocationCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<CompanyLocationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] bool includeInactive = false,
        [FromQuery] string? companyId = null,
        [FromQuery] string? locationId = null)
    {
        var query = new GetAllCompanyLocationsQuery
        {
            IncludeInactive = includeInactive,
            CompanyId = companyId,
            LocationId = locationId
        };

        var items = await _queryHandler.Handle(query);
        return Ok(items);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CompanyLocationDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetCompanyLocationByIdQuery { Id = id };
        var item = await _queryHandler.Handle(query);

        if (item == null)
            return NotFound(new { message = $"CompanyLocation with ID {id} not found" });

        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCompanyLocationCommand command)
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
    public async Task<IActionResult> Update(string id, [FromBody] UpdateCompanyLocationCommand command)
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
        var command = new DeleteCompanyLocationCommand { Id = id };

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
