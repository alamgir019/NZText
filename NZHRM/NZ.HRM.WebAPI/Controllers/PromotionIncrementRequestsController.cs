using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using NZ.Payroll.Application.PromotionIncrementRequests.Commands;
using NZ.Payroll.Application.PromotionIncrementRequests.Handlers;
using NZ.Payroll.Application.PromotionIncrementRequests.Queries;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/payroll/promotion-increment-requests")]
public class PromotionIncrementRequestsController : ControllerBase
{
	private readonly ForwardPromotionIncrementRequestsToDirectorHandler _forwardToDirectorHandler;
	private readonly GetPromotionIncrementApprovalHistoryHandler _getApprovalHistoryHandler;
	private readonly GetPromotionIncrementRequestsByStatusHandler _getByStatusHandler;
	private readonly ForwardPromotionIncrementRequestsToMovementCellHandler _forwardToMovementCellHandler;
	private readonly ForwardPromotionIncrementRequestsToHrBranchManagerHandler _forwardToHrBranchManagerHandler;
	private readonly ForwardPromotionIncrementRequestsToCeoHandler _forwardToCeoHandler;

	public PromotionIncrementRequestsController(
		ForwardPromotionIncrementRequestsToDirectorHandler forwardToDirectorHandler,
		GetPromotionIncrementApprovalHistoryHandler getApprovalHistoryHandler,
		GetPromotionIncrementRequestsByStatusHandler getByStatusHandler,
		ForwardPromotionIncrementRequestsToMovementCellHandler forwardToMovementCellHandler,
		ForwardPromotionIncrementRequestsToHrBranchManagerHandler forwardToHrBranchManagerHandler,
		ForwardPromotionIncrementRequestsToCeoHandler forwardToCeoHandler)
	{
		_forwardToDirectorHandler = forwardToDirectorHandler;
		_getApprovalHistoryHandler = getApprovalHistoryHandler;
		_getByStatusHandler = getByStatusHandler;
		_forwardToMovementCellHandler = forwardToMovementCellHandler;
		_forwardToHrBranchManagerHandler = forwardToHrBranchManagerHandler;
		_forwardToCeoHandler = forwardToCeoHandler;
	}

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> GetByStatus(
		[FromQuery] string? status,
		CancellationToken cancellationToken = default)
	{
		try
		{
			var result = await _getByStatusHandler.Handle(
				new GetPromotionIncrementRequestsByStatusQuery { Status = status ?? string.Empty },
				cancellationToken);

			return Ok(new
			{
				items = result,
				totalCount = result.Count,
				message = "Promotion increment requests retrieved"
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
				message = "An error occurred while retrieving promotion increment requests",
				details = ex.Message
			});
		}
	}

	[HttpPut("forward-to-movement-section")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<IActionResult> ForwardToMovementSection(
		[FromBody] ForwardPromotionIncrementRequestsToMovementCellCommand command,
		CancellationToken cancellationToken = default)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
		if (string.IsNullOrWhiteSpace(currentUser))
			return Unauthorized(new { message = "Authenticated user was not found" });

		try
		{
			var result = await _forwardToMovementCellHandler.Handle(command, currentUser, cancellationToken);

			return Ok(new
			{
				data = result,
				message = "Promotion increment requests forwarded to Attendance & Workforce Movement Section"
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
				message = "An error occurred while forwarding promotion increment requests to Attendance & Workforce Movement Section",
				details = ex.Message
			});
		}
	}

	[HttpPut("forward-to-hr-branch-manager")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<IActionResult> ForwardToHrBranchManager(
		[FromBody] ForwardPromotionIncrementRequestsToHrBranchManagerCommand command,
		CancellationToken cancellationToken = default)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
		if (string.IsNullOrWhiteSpace(currentUser))
			return Unauthorized(new { message = "Authenticated user was not found" });

		try
		{
			var result = await _forwardToHrBranchManagerHandler.Handle(command, currentUser, cancellationToken);

			return Ok(new
			{
				data = result,
				message = "Promotion increment requests forwarded to HR Branch Manager"
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
				message = "An error occurred while forwarding promotion increment requests to HR Branch Manager",
				details = ex.Message
			});
		}
	}

	[HttpPut("forward-to-ceo")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<IActionResult> ForwardToCeo(
		[FromBody] ForwardPromotionIncrementRequestsToCeoCommand command,
		CancellationToken cancellationToken = default)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
		if (string.IsNullOrWhiteSpace(currentUser))
			return Unauthorized(new { message = "Authenticated user was not found" });

		try
		{
			var result = await _forwardToCeoHandler.Handle(command, currentUser, cancellationToken);

			return Ok(new
			{
				data = result,
				message = "Promotion increment requests forwarded to CEO for final approval"
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
				message = "An error occurred while forwarding promotion increment requests to CEO",
				details = ex.Message
			});
		}
	}

	[HttpPost("forward-to-director")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<IActionResult> ForwardToDirector(
		[FromBody] ForwardPromotionIncrementRequestsToDirectorCommand command,
		CancellationToken cancellationToken = default)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		try
		{
			var result = await _forwardToDirectorHandler.Handle(command, "System", cancellationToken);

			return Ok(new
			{
				data = result,
				message = "Promotion increment requests forwarded to Director"
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
				message = "An error occurred while forwarding promotion increment requests to Director",
				details = ex.Message
			});
		}
	}

	[HttpGet("{requestId}/approval-history")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetApprovalHistory(
		string requestId,
		CancellationToken cancellationToken = default)
	{
		try
		{
			var result = await _getApprovalHistoryHandler.Handle(
				new GetPromotionIncrementApprovalHistoryQuery { RequestId = requestId },
				cancellationToken);

			return Ok(new
			{
				items = result,
				message = "Promotion increment approval history retrieved"
			});
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
			return StatusCode(500, new
			{
				message = "An error occurred while retrieving promotion increment approval history",
				details = ex.Message
			});
		}
	}
}
