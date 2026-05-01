using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Grades.Commands.CreateGrade;
using NZ.HRM.Application.Grades.Commands.DeleteGrade;
using NZ.HRM.Application.Grades.Commands.UpdateGrade;
using NZ.HRM.Application.Grades.Handlers;
using NZ.HRM.Application.Grades.Queries.GetAllGrades;
using NZ.HRM.Application.Grades.Queries.GetGradeById;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradesController : ControllerBase
{
    private readonly GradeQueryHandler _queryHandler;
    private readonly GradeCommandHandler _commandHandler;

    public GradesController(
        GradeQueryHandler queryHandler,
        GradeCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    /// <summary>
    /// Get all grades
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<GradeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
    {
        var query = new GetAllGradesQuery { IncludeInactive = includeInactive };
        var grades = await _queryHandler.Handle(query);
        return Ok(grades);
    }

    /// <summary>
    /// Get grade by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GradeDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetGradeByIdQuery { Id = id };
        var grade = await _queryHandler.Handle(query);

        if (grade == null)
            return NotFound(new { message = $"Grade with ID {id} not found" });

        return Ok(grade);
    }

    /// <summary>
    /// Create a new grade
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateGradeCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var gradeId = await _commandHandler.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id = gradeId }, new { id = gradeId });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Update an existing grade
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateGradeCommand command)
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
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Delete a grade (soft delete)
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeleteGradeCommand { Id = id };

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
