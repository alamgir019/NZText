using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.RawPunches.Commands.CreateRawPunch;
using NZ.Attendance.Application.RawPunches.Handlers;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RawPunchesController : ControllerBase
{
    private readonly RawPunchCommandHandler _commandHandler;

    public RawPunchesController(RawPunchCommandHandler commandHandler)
    {
        _commandHandler = commandHandler;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateRawPunchResultDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateRawPunchCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _commandHandler.Handle(command);
        return CreatedAtAction(null, new { id = result.RawPunchId }, result);
    }
}
