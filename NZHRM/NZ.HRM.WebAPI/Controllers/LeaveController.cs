using Microsoft.AspNetCore.Mvc;
using NZ.Leave.Application.LeaveRequests.Commands.CreateLeaveRequests;
using NZ.Leave.Application.LeaveRequests.Commands.DeleteLeaveRequest;
using NZ.Leave.Application.LeaveRequests.Commands.UpdateLeaveRequest;
using NZ.Leave.Application.LeaveRequests.Queries.GetLeaveRequests;
using NZ.Leave.Application.LeaveTypes.Handlers;
using NZ.Leave.Application.LeaveTypes.Queries.GetAllLeaveTypes;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveController : ControllerBase
{
    private readonly GetAllLeaveTypesQueryHandler _getAllLeaveTypesHandler;
    private readonly CreateLeaveRequestsCommandHandler _createHandler;
    private readonly UpdateLeaveRequestCommandHandler _updateHandler;
    private readonly DeleteLeaveRequestCommandHandler _deleteHandler;
    private readonly GetLeaveRequestsQueryHandler _getAllHandler;


    public LeaveController(
        GetAllLeaveTypesQueryHandler getAllLeaveTypesHandler,
        CreateLeaveRequestsCommandHandler createHandler,
        UpdateLeaveRequestCommandHandler updateHandler,
        DeleteLeaveRequestCommandHandler deleteHandler,
        GetLeaveRequestsQueryHandler getAllHandler)
    {
        _getAllLeaveTypesHandler = getAllLeaveTypesHandler;
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
        _getAllHandler = getAllHandler;
    }

    [HttpGet("types")]
    public async Task<IActionResult> GetLeaveTypes()
    {
        var leaveTypes = await _getAllLeaveTypesHandler.Handle(new GetAllLeaveTypesQuery());
        return Ok(leaveTypes);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLeaveRequestsCommand command, CancellationToken cancellationToken)
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
        var query = new GetLeaveRequestsQuery { Status = status, Page = page, Size = size };
        var (items, total) = await _getAllHandler.Handle(query, cancellationToken);

        return Ok(new
        {
            success = true,
            data = items,
            total
        });
    }

    [HttpPut("{requestId}")]
    public async Task<IActionResult> Update(string requestId, [FromBody] UpdateLeaveRequestCommand command, CancellationToken cancellationToken)
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
        var result = await _deleteHandler.Handle(new DeleteLeaveRequestCommand { RequestId = requestId }, cancellationToken);

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
