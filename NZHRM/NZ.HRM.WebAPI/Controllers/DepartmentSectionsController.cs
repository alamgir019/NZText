using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.DepartmentSections.Commands.CreateDepartmentSection;
using NZ.HRM.Application.DepartmentSections.Commands.DeleteDepartmentSection;
using NZ.HRM.Application.DepartmentSections.Commands.UpdateDepartmentSection;
using NZ.HRM.Application.DepartmentSections.Handlers;
using NZ.HRM.Application.DepartmentSections.Queries.GetAllDepartmentSections;
using NZ.HRM.Application.DepartmentSections.Queries.GetDepartmentSectionById;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentSectionsController : ControllerBase
{
    private readonly DepartmentSectionQueryHandler _queryHandler;
    private readonly DepartmentSectionCommandHandler _commandHandler;

    public DepartmentSectionsController(
        DepartmentSectionQueryHandler queryHandler,
        DepartmentSectionCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<DepartmentSectionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] bool includeInactive = false,
        [FromQuery] string? departmentId = null,
        [FromQuery] string? sectionId = null)
    {
        var query = new GetAllDepartmentSectionsQuery
        {
            IncludeInactive = includeInactive,
            DepartmentId = departmentId,
            SectionId = sectionId
        };

        var items = await _queryHandler.Handle(query);
        return Ok(items);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DepartmentSectionDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetDepartmentSectionByIdQuery { Id = id };
        var item = await _queryHandler.Handle(query);

        if (item == null)
            return NotFound(new { message = $"DepartmentSection with ID {id} not found" });

        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentSectionCommand command)
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
    public async Task<IActionResult> Update(string id, [FromBody] UpdateDepartmentSectionCommand command)
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
        var command = new DeleteDepartmentSectionCommand { Id = id };

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
