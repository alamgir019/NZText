using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using NZ.Leave.Application.LeaveEncashmentRequests.Commands.CreateLeaveEncashmentRequests;
using NZ.Leave.Application.LeaveEncashmentRequests.Commands.DeleteLeaveEncashmentRequest;
using NZ.Leave.Application.LeaveEncashmentRequests.Commands.ProcessLeaveEncashmentAction;
using NZ.Leave.Application.LeaveEncashmentRequests.Commands.UpdateLeaveEncashmentRequest;
using NZ.Leave.Application.LeaveEncashmentRequests.Queries.GetLeaveEncashmentRequestDetail;
using NZ.Leave.Application.LeaveEncashmentRequests.Queries.GetLeaveEncashmentRequests;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/v1/leave-encashment-requests")]
public class LeaveEncashmentRequestsController : ControllerBase
{
    private readonly CreateLeaveEncashmentRequestsCommandHandler _createHandler;
    private readonly UpdateLeaveEncashmentRequestCommandHandler _updateHandler;
    private readonly DeleteLeaveEncashmentRequestCommandHandler _deleteHandler;
    private readonly GetLeaveEncashmentRequestsQueryHandler _getAllHandler;
    private readonly GetLeaveEncashmentRequestDetailQueryHandler _getDetailHandler;
    private readonly ProcessLeaveEncashmentActionCommandHandler _processActionHandler;

    public LeaveEncashmentRequestsController(
        CreateLeaveEncashmentRequestsCommandHandler createHandler,
        UpdateLeaveEncashmentRequestCommandHandler updateHandler,
        DeleteLeaveEncashmentRequestCommandHandler deleteHandler,
        GetLeaveEncashmentRequestsQueryHandler getAllHandler,
        GetLeaveEncashmentRequestDetailQueryHandler getDetailHandler,
        ProcessLeaveEncashmentActionCommandHandler processActionHandler)
    {
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
        _getAllHandler = getAllHandler;
        _getDetailHandler = getDetailHandler;
        _processActionHandler = processActionHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLeaveEncashmentRequestsCommand command, CancellationToken cancellationToken)
    {
        var result = await _createHandler.Handle(command, cancellationToken);
        if (!result.Success)
        {
            return BadRequest(new
            {
                success = false,
                errorCode = result.ErrorCode,
                message = result.Message
            });
        }

        return Ok(new
        {
            success = true,
            message = result.Message,
            totalEmployees = result.TotalEmployees,
            totalDays = result.TotalDays
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? status, [FromQuery] string? instalment, [FromQuery] string? leaveType, [FromQuery] int page = 1, [FromQuery] int size = 20, CancellationToken cancellationToken = default)
    {
        var query = new GetLeaveEncashmentRequestsQuery { Status = status, Instalment = instalment, LeaveType = leaveType, Page = page, Size = size };
        var (items, total) = await _getAllHandler.Handle(query, cancellationToken);

        return Ok(new
        {
            success = true,
            data = items,
            total
        });
    }

    [HttpGet("{requestId}")]
    public async Task<IActionResult> GetById(string requestId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(requestId))
        {
            return BadRequest(new
            {
                success = false,
                errorCode = "INVALID_REQUEST",
                message = "Request ID is mandatory."
            });
        }

        var detail = await _getDetailHandler.Handle(
            new GetLeaveEncashmentRequestDetailQuery { RequestId = requestId },
            cancellationToken);

        if (detail == null)
        {
            return NotFound(new
            {
                success = false,
                errorCode = "REQUEST_NOT_FOUND",
                message = "Leave encashment request not found."
            });
        }

        return Ok(detail);
    }

    [HttpPost("action")]
    public async Task<IActionResult> ProcessAction([FromBody] ProcessLeaveEncashmentActionCommand command, CancellationToken cancellationToken = default)
    {
        command.ProcessedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var result = await _processActionHandler.Handle(command, cancellationToken);
        if (!result.Success)
        {
            var statusCode = result.ErrorCode == "REQUEST_NOT_FOUND"
                ? StatusCodes.Status404NotFound
                : StatusCodes.Status400BadRequest;

            return StatusCode(statusCode, new
            {
                success = false,
                errorCode = result.ErrorCode,
                message = result.Message
            });
        }

        return Ok(new
        {
            success = true,
            requestId = result.RequestId,
            action = result.Action,
            status = result.Status,
            message = result.Message
        });
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] List<UpdateLeaveEncashmentRequestCommand> commands, CancellationToken cancellationToken)
    {
        var result = await _updateHandler.Handle(commands, cancellationToken);

        if (!result.Success)
        {
            var statusCode = result.ErrorCode == "VAL-NOTFOUND" ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest;
            return StatusCode(statusCode, new
            {
                success = false,
                errorCode = result.ErrorCode,
                message = result.Message
            });
        }

        return Ok(new
        {
            success = true,
            message = result.Message
        });
    }

    [HttpDelete("{requestId}")]
    public async Task<IActionResult> Delete(string requestId, CancellationToken cancellationToken)
    {
        var result = await _deleteHandler.Handle(new DeleteLeaveEncashmentRequestCommand { RequestId = requestId }, cancellationToken);

        if (!result.Success)
        {
            var statusCode = result.ErrorCode == "VAL-NOTFOUND" ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest;
            return StatusCode(statusCode, new
            {
                success = false,
                errorCode = result.ErrorCode,
                message = result.Message
            });
        }

        return Ok(new
        {
            success = true,
            message = result.Message
        });
    }
}
