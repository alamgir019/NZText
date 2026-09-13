using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.EmployeeShiftChanges.Commands.CreateEmployeeShiftChange;
using NZ.HRM.Application.EmployeeShiftChanges.Commands.DeleteEmployeeShiftChange;
using NZ.HRM.Application.EmployeeShiftChanges.Commands.UpdateEmployeeShiftChange;
using NZ.HRM.Application.EmployeeShiftChanges.Handlers;
using NZ.HRM.Application.EmployeeShiftChanges.Queries.GetAllEmployeeShiftChanges;
using NZ.HRM.Application.EmployeeShiftChanges.Queries.GetEmployeeShiftChangeById;
using NZ.HRM.Application.EmployeeShiftChanges.Queries.GetEmployeeShiftChangesByEmployee;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeShiftChangesController : ControllerBase
{
	private readonly EmployeeShiftChangeQueryHandler _queryHandler;
	private readonly EmployeeShiftChangeCommandHandler _commandHandler;

	public EmployeeShiftChangesController(
		EmployeeShiftChangeQueryHandler queryHandler,
		EmployeeShiftChangeCommandHandler commandHandler)
	{
		_queryHandler = queryHandler;
		_commandHandler = commandHandler;
	}

	// Get all employee shift changes with optional filters for inactive records, employee ID, and date range
	[HttpGet]
	[ProducesResponseType(typeof(List<EmployeeShiftChangeDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetAll(
		[FromQuery] bool includeInactive = false,
		[FromQuery] string? employeeId = null,
		[FromQuery] DateOnly? fromDate = null,
		[FromQuery] DateOnly? toDate = null,
		CancellationToken cancellationToken = default)
	{
		var query = new GetAllEmployeeShiftChangesQuery
		{
			IncludeInactive = includeInactive,
			EmployeeId = employeeId,
			FromDate = fromDate,
			ToDate = toDate
		};

		var changes = await _queryHandler.Handle(query, cancellationToken);
		return Ok(changes);
	}

	// Get a specific employee shift change by ID
	[HttpGet("{id}")]
	[ProducesResponseType(typeof(EmployeeShiftChangeDetailDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken = default)
	{
		var query = new GetEmployeeShiftChangeByIdQuery { Id = id };
		var change = await _queryHandler.Handle(query, cancellationToken);

		if (change == null)
			return NotFound(new { message = $"Employee shift change with ID {id} not found" });

		return Ok(change);
	}

	// Get all shift changes for a specific employee with optional filters for inactive records and date range
	[HttpGet("employee/{employeeId}")]
	[ProducesResponseType(typeof(List<EmployeeShiftChangeDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetByEmployee(
		string employeeId,
		[FromQuery] bool includeInactive = false,
		[FromQuery] DateOnly? fromDate = null,
		[FromQuery] DateOnly? toDate = null,
		CancellationToken cancellationToken = default)
	{
		var query = new GetEmployeeShiftChangesByEmployeeQuery
		{
			EmployeeId = employeeId,
			IncludeInactive = includeInactive,
			FromDate = fromDate,
			ToDate = toDate
		};

		var changes = await _queryHandler.Handle(query, cancellationToken);
		return Ok(changes);
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Create(
		[FromBody] CreateEmployeeShiftChangeCommand command,
		CancellationToken cancellationToken = default)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		try
		{
			var id = await _commandHandler.Handle(command, cancellationToken);
			return CreatedAtAction(nameof(GetById), new { id }, new { id });
		}
		catch (KeyNotFoundException ex)
		{
			return NotFound(new { message = ex.Message });
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
		catch (InvalidOperationException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}

	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Update(
		string id,
		[FromBody] UpdateEmployeeShiftChangeCommand command,
		CancellationToken cancellationToken = default)
	{
		if (id != command.Id)
			return BadRequest(new { message = "ID mismatch" });

		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		try
		{
			await _commandHandler.Handle(command, cancellationToken);
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
		catch (InvalidOperationException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}

	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Delete(
		string id,
		CancellationToken cancellationToken = default)
	{
		var command = new DeleteEmployeeShiftChangeCommand { Id = id };

		try
		{
			await _commandHandler.Handle(command, cancellationToken);
			return NoContent();
		}
		catch (KeyNotFoundException ex)
		{
			return NotFound(new { message = ex.Message });
		}
		catch (InvalidOperationException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}
}
