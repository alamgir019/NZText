using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.MedicalFitnessChecks.Commands.CreateMedicalFitnessCheck;
using NZ.HRM.Application.MedicalFitnessChecks.Commands.DeleteMedicalFitnessCheck;
using NZ.HRM.Application.MedicalFitnessChecks.Commands.UpdateMedicalFitnessCheck;
using NZ.HRM.Application.MedicalFitnessChecks.Handlers;
using NZ.HRM.Application.MedicalFitnessChecks.Queries.GetAllMedicalFitnessChecks;
using NZ.HRM.Application.MedicalFitnessChecks.Queries.GetMedicalFitnessCheckById;
using NZ.HRM.Application.MedicalFitnessChecks.Queries.GetMedicalFitnessHistoryByEmployeeId;
using NZ.HRM.Application.MedicalFitnessChecks.Queries.GetMedicalFitnessReportByEmployeeId;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicalFitnessChecksController : ControllerBase
{
    private readonly MedicalFitnessCheckQueryHandler _queryHandler;
    private readonly MedicalFitnessCheckCommandHandler _commandHandler;

    public MedicalFitnessChecksController(
        MedicalFitnessCheckQueryHandler queryHandler,
        MedicalFitnessCheckCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<MedicalFitnessCheckDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
    {
        var query = new GetAllMedicalFitnessChecksQuery { IncludeInactive = includeInactive };
        var items = await _queryHandler.Handle(query);
        return Ok(items);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MedicalFitnessCheckDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetMedicalFitnessCheckByIdQuery { Id = id };
        var item = await _queryHandler.Handle(query);

        if (item == null)
            return NotFound(new { message = $"Medical fitness check with ID {id} not found" });

        return Ok(item);
    }

    [HttpGet("history/employee/{employeeId}")]
    [ProducesResponseType(typeof(List<MedicalFitnessHistoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHistoryByEmployeeId(string employeeId)
    {
        var query = new GetMedicalFitnessHistoryByEmployeeIdQuery { EmployeeId = employeeId };
        var items = await _queryHandler.Handle(query);
        return Ok(items);
    }

    [HttpGet("report/employee/{employeeId}")]
    [ProducesResponseType(typeof(MedicalFitnessReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReportByEmployeeId(string employeeId)
    {
        var query = new GetMedicalFitnessReportByEmployeeIdQuery { EmployeeId = employeeId };
        var item = await _queryHandler.Handle(query);

        if (item == null)
            return NotFound(new { message = $"Medical fitness report data not found for employee ID {employeeId}" });

        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] List<CreateMedicalFitnessCheckCommand> commands)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (commands == null || commands.Count == 0)
            return BadRequest(new { message = "At least one medical fitness check is required." });

        var ids = await _commandHandler.Handle(commands);
        return Created(string.Empty, new { ids });
    }

    [HttpPut("employee/{employeeId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string employeeId, [FromBody] UpdateMedicalFitnessCheckCommand command)
    {
        if (employeeId != command.EmployeeId)
            return BadRequest(new { message = "Employee ID mismatch" });

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
        var command = new DeleteMedicalFitnessCheckCommand { Id = id };

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
