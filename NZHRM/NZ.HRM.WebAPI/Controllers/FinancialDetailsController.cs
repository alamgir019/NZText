using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.FinancialDetails.Commands.CreateFinancialDetail;
using NZ.HRM.Application.FinancialDetails.Commands.DeleteFinancialDetail;
using NZ.HRM.Application.FinancialDetails.Commands.UpdateFinancialDetail;
using NZ.HRM.Application.FinancialDetails.Handlers;
using NZ.HRM.Application.FinancialDetails.Queries.GetAllFinancialDetails;
using NZ.HRM.Application.FinancialDetails.Queries.GetFinancialDetailById;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FinancialDetailsController : ControllerBase
{
    private readonly FinancialDetailQueryHandler _queryHandler;
    private readonly FinancialDetailCommandHandler _commandHandler;

    public FinancialDetailsController(
        FinancialDetailQueryHandler queryHandler,
        FinancialDetailCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<FinancialDetailDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false, [FromQuery] string? employeeId = null)
    {
        var query = new GetAllFinancialDetailsQuery { IncludeInactive = includeInactive, EmployeeId = employeeId };
        var items = await _queryHandler.Handle(query);
        return Ok(items);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(FinancialDetailDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetFinancialDetailByIdQuery { Id = id };
        var item = await _queryHandler.Handle(query);

        if (item == null)
            return NotFound(new { message = $"Financial detail with ID {id} not found" });

        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateFinancialDetailCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _commandHandler.Handle(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateFinancialDetailCommand command)
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
        var command = new DeleteFinancialDetailCommand { Id = id };

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
