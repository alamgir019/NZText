using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using NZ.Payroll.Application.Interfaces;
using NZ.Payroll.Application.PayIncrementHistories.Commands;
using NZ.Payroll.Application.PayIncrementHistories.DTOs;
using NZ.Payroll.Application.PayIncrementHistories.Handlers;
using NZ.Payroll.Application.PayIncrementHistories.Queries;
using NZ.Payroll.Domain.Contracts;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/payroll")]
public class PayrollController : ControllerBase
{
	private readonly CreatePayIncrementHistoryHandler _createPayIncrementHistoryHandler;
	private readonly UpdatePayIncrementHistoryHandler _updatePayIncrementHistoryHandler;
	private readonly GetPayIncrementHistoriesByStatusHandler _getPayIncrementHistoriesByStatusHandler;

	public PayrollController(
		CreatePayIncrementHistoryHandler createPayIncrementHistoryHandler,
		UpdatePayIncrementHistoryHandler updatePayIncrementHistoryHandler,
		GetPayIncrementHistoriesByStatusHandler getPayIncrementHistoriesByStatusHandler)
	{
		_createPayIncrementHistoryHandler = createPayIncrementHistoryHandler;
		_updatePayIncrementHistoryHandler = updatePayIncrementHistoryHandler;
		_getPayIncrementHistoriesByStatusHandler = getPayIncrementHistoriesByStatusHandler;
	}

	[HttpGet("increment-histories")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> GetIncrementHistoriesByStatus(
		[FromQuery] string? status,
		CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(status))
			return BadRequest(new { message = "Status is required" });

		try
		{
			var query = new GetPayIncrementHistoriesByStatusQuery
			{
				Status = status
			};

			var result = await _getPayIncrementHistoriesByStatusHandler.Handle(
				query,
				cancellationToken);

			return Ok(new
			{
				items = result,
				message = "Pay increment histories retrieved"
			});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
		catch (Exception ex)
		{
			return StatusCode(500, new
			{
				message = "An error occurred while retrieving approved pay increment histories",
				details = ex.Message
			});
		}
	}

	[HttpPost("increment-histories")]
	[ProducesResponseType(typeof(PayIncrementHistoryDto), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<IActionResult> CreateIncrementHistory(
		[FromBody] CreateIncrementRequestsCommand command,
		CancellationToken cancellationToken = default)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
		if (string.IsNullOrWhiteSpace(currentUser))
			return Unauthorized(new { message = "Authenticated user was not found" });

		command.CreatedBy = currentUser;

		try
		{
			var Ids = await _createPayIncrementHistoryHandler.Handle(command, cancellationToken);
			return CreatedAtAction(
				nameof(CreateIncrementHistory),
				new { id = Ids },
				new { id = Ids, message = "Pay Increment History created" });
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
		catch (KeyNotFoundException ex)
		{
			return NotFound(new { message = ex.Message });
		}
		catch (Exception ex)
		{
			return StatusCode(500, new { message = "An error occurred while creating the pay increment history", details = ex.Message });
		}
	}

	[HttpPut("increment-histories/")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<IActionResult> UpdateIncrementHistory(
		[FromBody] UpdateIncrementRequestsCommand command,
		CancellationToken cancellationToken = default)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
		if (string.IsNullOrWhiteSpace(currentUser))
			return Unauthorized(new { message = "Authenticated user was not found" });

		try
		{
			var result = await _updatePayIncrementHistoryHandler.Handle(
				command,
				currentUser,
				cancellationToken);

			return Ok(new
			{
				items = result.Select(item => new
				{
					id = item.Id,
					requestId = item.RequestId,
					employeeId = item.EmployeeId
				}),
				message = "Pay Increment Histories updated and requests created"
			});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
		catch (UnauthorizedAccessException ex)
		{
			return Unauthorized(new { message = ex.Message });
		}
		catch (KeyNotFoundException ex)
		{
			return NotFound(new { message = ex.Message });
		}
		catch (InvalidOperationException ex)
		{
			return Conflict(new { message = ex.Message });
		}
		catch (Exception ex)
		{
			return StatusCode(500, new
			{
				message = "An error occurred while updating the pay increment history",
				details = ex.Message
			});
		}
	}

	
}
