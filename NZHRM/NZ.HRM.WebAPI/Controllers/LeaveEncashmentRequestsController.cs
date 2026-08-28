using Microsoft.AspNetCore.Mvc;
using NZ.Leave.Application.LeaveEncashmentRequests.Commands.CreateLeaveEncashmentRequests;
using NZ.Leave.Application.LeaveEncashmentRequests.Commands.DeleteLeaveEncashmentRequest;
using NZ.Leave.Application.LeaveEncashmentRequests.Commands.UpdateLeaveEncashmentRequest;
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

    public LeaveEncashmentRequestsController(
        CreateLeaveEncashmentRequestsCommandHandler createHandler,
        UpdateLeaveEncashmentRequestCommandHandler updateHandler,
        DeleteLeaveEncashmentRequestCommandHandler deleteHandler,
        GetLeaveEncashmentRequestsQueryHandler getAllHandler)
    {
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
        _getAllHandler = getAllHandler;
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
    public async Task<IActionResult> GetAll([FromQuery] string? status, [FromQuery] int page = 1, [FromQuery] int size = 20, CancellationToken cancellationToken = default)
    {
        var query = new GetLeaveEncashmentRequestsQuery { Status = status, Page = page, Size = size };
        var (items, total) = await _getAllHandler.Handle(query, cancellationToken);

        return Ok(new
        {
            success = true,
            data = items,
            total
        });
    }

    [HttpPut("{requestId}")]
    public async Task<IActionResult> Update(string requestId, [FromBody] UpdateLeaveEncashmentRequestCommand command, CancellationToken cancellationToken)
    {
        command.RequestId = requestId;
        var result = await _updateHandler.Handle(command, cancellationToken);

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
