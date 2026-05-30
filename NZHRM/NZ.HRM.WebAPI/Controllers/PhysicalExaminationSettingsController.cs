using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.PhysicalExaminationSettings.Commands.CreatePhysicalExaminationSetting;
using NZ.HRM.Application.PhysicalExaminationSettings.Commands.DeletePhysicalExaminationSetting;
using NZ.HRM.Application.PhysicalExaminationSettings.Commands.UpdatePhysicalExaminationSetting;
using NZ.HRM.Application.PhysicalExaminationSettings.Handlers;
using NZ.HRM.Application.PhysicalExaminationSettings.Queries.GetAllPhysicalExaminationSettings;
using NZ.HRM.Application.PhysicalExaminationSettings.Queries.GetPhysicalExaminationSettingById;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhysicalExaminationSettingsController : ControllerBase
{
    private readonly PhysicalExaminationSettingQueryHandler _queryHandler;
    private readonly PhysicalExaminationSettingCommandHandler _commandHandler;

    public PhysicalExaminationSettingsController(
        PhysicalExaminationSettingQueryHandler queryHandler,
        PhysicalExaminationSettingCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<PhysicalExaminationSettingDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
    {
        var query = new GetAllPhysicalExaminationSettingsQuery { IncludeInactive = includeInactive };
        var items = await _queryHandler.Handle(query);
        return Ok(items);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PhysicalExaminationSettingDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetPhysicalExaminationSettingByIdQuery { Id = id };
        var item = await _queryHandler.Handle(query);

        if (item == null)
            return NotFound(new { message = $"Physical examination setting with ID {id} not found" });

        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePhysicalExaminationSettingCommand command)
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
    public async Task<IActionResult> Update(string id, [FromBody] UpdatePhysicalExaminationSettingCommand command)
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
        var command = new DeletePhysicalExaminationSettingCommand { Id = id };

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
