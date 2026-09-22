using Microsoft.AspNetCore.Mvc;
using NZ.Leave.Application.LeaveRequests.Commands.CreateLeaveRequests;
using NZ.Leave.Application.LeaveRequests.Commands.DeleteLeaveRequest;
using NZ.Leave.Application.LeaveRequests.Commands.UpdateLeaveRequest;
using NZ.Leave.Application.LeaveRequests.Queries.GetLeaveRequests;
using NZ.Leave.Application.LeaveTypes.Handlers;
using NZ.Leave.Application.Interfaces.Repositories;
using NZ.Leave.Application.LeaveRequests.Queries.GetLeaveRequestById;
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
    private readonly GetLeaveRequestByIdQueryHandler _getByIdHandler;


    public LeaveController(
        GetAllLeaveTypesQueryHandler getAllLeaveTypesHandler,
        CreateLeaveRequestsCommandHandler createHandler,
        UpdateLeaveRequestCommandHandler updateHandler,
        DeleteLeaveRequestCommandHandler deleteHandler,
        GetLeaveRequestsQueryHandler getAllHandler,
        GetLeaveRequestByIdQueryHandler getByIdHandler)
    {
        _getAllLeaveTypesHandler = getAllLeaveTypesHandler;
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
        _getAllHandler = getAllHandler;
        _getByIdHandler = getByIdHandler;
    }

    [HttpGet("/api/v1/leave-requests/{requestId}")]
    public async Task<IActionResult> GetById(string requestId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(requestId))
        {
            return BadRequest(new
            {
                code = "INVALID_REQUEST_ID",
                message = "Invalid leave request identifier."
            });
        }

        var dto = await _getByIdHandler.Handle(new GetLeaveRequestByIdQuery { RequestId = requestId }, cancellationToken);

        if (dto == null)
        {
            return NotFound(new
            {
                code = "LEAVE_REQUEST_NOT_FOUND",
                message = "Leave request not found."
            });
        }

        var response = new
        {
            requestId = dto.RequestId,
            status = dto.Status,
            appliedOn = dto.CreatedDate?.ToString("o"),
            forwardedBy = dto.ForwardedBy == null ? null : new
            {
                employeeId = dto.ForwardedBy,
                name = string.Empty,
                forwardedOn = dto.ForwardedDate?.ToString("o")
            },
            employee = new
            {
                employeeId = dto.EmployeeId,
                employeeName = dto.EmployeeName,
                department = dto.DepartmentName,
                designation = string.Empty,
                dateOfJoining = (string?)null,
                reportingManager = (object?)null
            },
            leave = new
            {
                leaveType = dto.LeaveType,
                leaveCode = dto.LeaveTypeId,
                startDate = dto.FromDate.ToString("yyyy-MM-dd"),
                endDate = dto.ToDate.ToString("yyyy-MM-dd"),
                totalDays = dto.TotalDays,
                session = "FULL_DAY",
                reason = dto.Reason,
                contactNumber = string.Empty
            }
        };

        return Ok(response);
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
    public async Task<IActionResult> GetAll([FromQuery] string? status, [FromQuery] DateOnly? fromDate, [FromQuery] DateOnly? toDate, [FromQuery] int page = 1, [FromQuery] int size = 20, CancellationToken cancellationToken = default)
    {
        var query = new GetLeaveRequestsQuery { Status = status, FromDate = fromDate, ToDate = toDate, Page = page, Size = size };
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
