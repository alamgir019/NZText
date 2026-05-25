using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.EmployeeNatures.Commands.CreateEmployeeNature;
using NZ.HRM.Application.EmployeeNatures.Commands.DeleteEmployeeNature;
using NZ.HRM.Application.EmployeeNatures.Commands.UpdateEmployeeNature;
using NZ.HRM.Application.EmployeeNatures.Handlers;
using NZ.HRM.Application.EmployeeNatures.Queries.GetAllEmployeeNatures;
using NZ.HRM.Application.EmployeeNatures.Queries.GetEmployeeNatureById;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeNaturesController : ControllerBase
{
    private readonly EmployeeNatureQueryHandler _queryHandler;
    private readonly EmployeeNatureCommandHandler _commandHandler;

    public EmployeeNaturesController(EmployeeNatureQueryHandler queryHandler, EmployeeNatureCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<EmployeeNatureDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
    {
        var query = new GetAllEmployeeNaturesQuery { IncludeInactive = includeInactive };
        var items = await _queryHandler.Handle(query);
        return Ok(items);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EmployeeNatureDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetEmployeeNatureByIdQuery { Id = id };
        var item = await _queryHandler.Handle(query);
        if (item == null) return NotFound(new { message = $"Employee nature with ID {id} not found" });
        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeNatureCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var id = await _commandHandler.Handle(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateEmployeeNatureCommand command)
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
        var command = new DeleteEmployeeNatureCommand { Id = id };
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
