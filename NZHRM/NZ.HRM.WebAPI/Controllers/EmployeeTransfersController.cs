using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.EmployeeTransfers.Commands.CreateEmployeeTransfer;
using NZ.HRM.Application.EmployeeTransfers.Commands.DeleteEmployeeTransfer;
using NZ.HRM.Application.EmployeeTransfers.Commands.UpdateEmployeeTransfer;
using NZ.HRM.Application.EmployeeTransfers.Handlers;
using NZ.HRM.Application.EmployeeTransfers.Queries.GetAllEmployeeTransfers;
using NZ.HRM.Application.EmployeeTransfers.Queries.GetEmployeeTransferById;
using NZ.HRM.Application.EmployeeTransfers.Queries.GetEmployeeTransfersByEmployee;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeTransfersController : ControllerBase
{
	private readonly EmployeeTransferQueryHandler _queryHandler;
	private readonly EmployeeTransferCommandHandler _commandHandler;

	public EmployeeTransfersController(
		EmployeeTransferQueryHandler queryHandler,
		EmployeeTransferCommandHandler commandHandler)
	{
		_queryHandler = queryHandler;
		_commandHandler = commandHandler;
	}

	// Get all employee transfers with optional filters for inactive records, employee ID, and date range 
	[HttpGet]
	[ProducesResponseType(typeof(List<EmployeeTransferDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetAll(
		[FromQuery] bool includeInactive = false,
		[FromQuery] string? employeeId = null,
		[FromQuery] DateOnly? fromDate = null,
		[FromQuery] DateOnly? toDate = null,
		CancellationToken cancellationToken = default)
	{
		var query = new GetAllEmployeeTransfersQuery
		{
			IncludeInactive = includeInactive,
			EmployeeId = employeeId,
			FromDate = fromDate,
			ToDate = toDate
		};

		var transfers = await _queryHandler.Handle(query, cancellationToken);
		return Ok(transfers);
	}

	// Get a specific employee transfer by ID
	[HttpGet("{id}")]
	[ProducesResponseType(typeof(EmployeeTransferDetailDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetById(
		string id,
		CancellationToken cancellationToken = default)
	{
		var query = new GetEmployeeTransferByIdQuery { Id = id };
		var transfer = await _queryHandler.Handle(query, cancellationToken);

		if (transfer == null)
			return NotFound(new { message = $"Employee transfer with ID {id} not found" });

		return Ok(transfer);
	}

	// Get all employee transfers for a specific employee with optional filters for inactive records and date range
	[HttpGet("employee/{employeeId}")]
	[ProducesResponseType(typeof(List<EmployeeTransferDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetByEmployee(
		string employeeId,
		[FromQuery] bool includeInactive = false,
		[FromQuery] DateOnly? fromDate = null,
		[FromQuery] DateOnly? toDate = null,
		CancellationToken cancellationToken = default)
	{
		var query = new GetEmployeeTransfersByEmployeeQuery
		{
			EmployeeId = employeeId,
			IncludeInactive = includeInactive,
			FromDate = fromDate,
			ToDate = toDate
		};

		var transfers = await _queryHandler.Handle(query, cancellationToken);
		return Ok(transfers);
	}

	// Create a new employee transfer 
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Create(
		[FromBody] CreateEmployeeTransferCommand command,
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

	// Update an existing employee transfer
	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Update(
		string id,
		[FromBody] UpdateEmployeeTransferCommand command,
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

	// Delete an existing employee transfer 
	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Delete(
		string id,
		CancellationToken cancellationToken = default)
	{
		var command = new DeleteEmployeeTransferCommand { Id = id };

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
