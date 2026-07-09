using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Banks.Commands.CreateBank;
using NZ.HRM.Application.Banks.Commands.UpdateBank;
using NZ.HRM.Application.Banks.Commands.DeleteBank;
using NZ.HRM.Application.Banks.Handlers;
using NZ.HRM.Application.Banks.Queries.GetAllBanks;
using NZ.HRM.Application.Banks.Queries.GetBankById;
using NZ.HRM.Application.Banks.Dto;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BanksController : ControllerBase
{
    private readonly BankQueryHandler _queryHandler;
    private readonly BankCommandHandler _commandHandler;

    public BanksController(BankQueryHandler queryHandler, BankCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<BankDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
    {
        var query = new GetAllBanksQuery { IncludeInactive = includeInactive };
        var banks = await _queryHandler.Handle(query);
        return Ok(banks);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BankDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetBankByIdQuery { Id = id };
        var bank = await _queryHandler.Handle(query);
        if (bank == null)
            return NotFound(new { message = $"Bank with ID {id} not found" });

        return Ok(bank);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateBankCommand command)
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
    public async Task<IActionResult> Update(string id, [FromBody] UpdateBankCommand command)
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
        var command = new DeleteBankCommand { Id = id };
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
