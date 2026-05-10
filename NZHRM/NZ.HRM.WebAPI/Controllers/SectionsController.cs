using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Sections.Commands.CreateSection;
using NZ.HRM.Application.Sections.Commands.DeleteSection;
using NZ.HRM.Application.Sections.Commands.UpdateSection;
using NZ.HRM.Application.Sections.Handlers;
using NZ.HRM.Application.Sections.Queries.GetAllSections;
using NZ.HRM.Application.Sections.Queries.GetSectionById;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SectionsController : ControllerBase
{
    private readonly SectionQueryHandler _queryHandler;
    private readonly SectionCommandHandler _commandHandler;

    public SectionsController(
        SectionQueryHandler queryHandler,
        SectionCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    /// <summary>
    /// Get all sections or filter by department
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<SectionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false, [FromQuery] string? departmentId = null)
    {
        var query = new GetAllSectionsQuery 
        { 
            IncludeInactive = includeInactive,
            DepartmentId = departmentId
        };
        var sections = await _queryHandler.Handle(query);
        return Ok(sections);
    }

    /// <summary>
    /// Get section by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SectionDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetSectionByIdQuery { Id = id };
        var section = await _queryHandler.Handle(query);

        if (section == null)
            return NotFound(new { message = $"Section with ID {id} not found" });

        return Ok(section);
    }

    /// <summary>
    /// Create a new section
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateSectionCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var sectionId = await _commandHandler.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id = sectionId }, new { id = sectionId });
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Update an existing section
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateSectionCommand command)
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

    /// <summary>
    /// Delete a section (soft delete)
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeleteSectionCommand { Id = id };

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
