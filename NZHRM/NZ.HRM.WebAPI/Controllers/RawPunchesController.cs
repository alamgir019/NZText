using Microsoft.AspNetCore.Mvc;
using NZ.Attendance.Application.RawPunches.Handlers;
using NZ.Attendance.Application.RawPunches.Queries.GetProcessedPunches;
using NZ.HRM.Application.RawPunches.Commands.CreateRawPunch;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RawPunchesController : ControllerBase
{
    private readonly RawPunchCommandHandler _commandHandler;
    private readonly QueryHandler _queryHandler;

    public RawPunchesController(
        RawPunchCommandHandler commandHandler,
        QueryHandler queryHandler)
    {
        _commandHandler = commandHandler;
        _queryHandler = queryHandler;
    }

    [HttpGet("processed-punches")]
    [ProducesResponseType(typeof(List<ProcessedPunchListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProcessedPunches(
        [FromQuery] string shiftId,
        [FromQuery] string companyId,
        [FromQuery] string punchType,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetProcessedPunchesQuery
            {
                ShiftId = shiftId,
                CompanyId = companyId,
                PunchType = punchType
            };

            var result = await _queryHandler.Handle(query, cancellationToken);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
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
