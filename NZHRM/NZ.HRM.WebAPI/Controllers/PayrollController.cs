using Microsoft.AspNetCore.Mvc;
using NZ.Payroll.Application.Interfaces;
using NZ.Payroll.Application.PayIncrementHistories.Commands;
using NZ.Payroll.Application.PayIncrementHistories.DTOs;
using NZ.Payroll.Application.PayIncrementHistories.Handlers;
using NZ.Payroll.Domain.Contracts;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/payroll")]
public class PayrollController : ControllerBase
{
	private readonly IPayrollProcessingService _payrollProcessingService;
	private readonly CreatePayIncrementHistoryHandler _createPayIncrementHistoryHandler;

	public PayrollController(
		IPayrollProcessingService payrollProcessingService,
		CreatePayIncrementHistoryHandler createPayIncrementHistoryHandler)
	{
		_payrollProcessingService = payrollProcessingService;
		_createPayIncrementHistoryHandler = createPayIncrementHistoryHandler;
	}

	[HttpPost("increment-histories")]
	[ProducesResponseType(typeof(PayIncrementHistoryDto), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<IActionResult> CreateIncrementHistory(
		[FromBody] CreatePayIncrementHistoryCommand command)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);
		try
		{
			var Id = await _createPayIncrementHistoryHandler.Handle(command, cancellationToken: default);
			return CreatedAtAction(
				nameof(CreateIncrementHistory),
				new { id = Id },
				new { id = Id, message = "Pay Increment History created" });
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

	
}
