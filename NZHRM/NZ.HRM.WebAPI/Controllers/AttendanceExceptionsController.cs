using Microsoft.AspNetCore.Mvc;
using NZ.Attendance.Application.AttendanceExceptions.Commands.CreateAttendanceExceptions;
using NZ.Attendance.Application.AttendanceExceptions.Commands.DeleteAttendanceException;
using NZ.Attendance.Application.AttendanceExceptions.Commands.ReviewAttendanceException;
using NZ.Attendance.Application.AttendanceExceptions.Commands.UpdateAttendanceException;
using NZ.Attendance.Application.AttendanceExceptions.Handlers;
using NZ.Attendance.Application.AttendanceExceptions.Queries.GetAllAttendanceExceptions;
using NZ.Attendance.Application.AttendanceExceptions.Queries.GetAttendanceExceptionById;
using NZ.Attendance.Domain.Enums;

namespace NZ.Attendance.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendanceExceptionsController : ControllerBase
{
    private readonly AttendanceExceptionCommandHandler _commandHandler;
    private readonly GetAttendanceExceptionByIdQueryHandler _getByIdHandler;
    private readonly GetAllAttendanceExceptionsQueryHandler _getAllHandler;

    public AttendanceExceptionsController(
        AttendanceExceptionCommandHandler commandHandler,
        GetAttendanceExceptionByIdQueryHandler getByIdHandler,
        GetAllAttendanceExceptionsQueryHandler getAllHandler)
    {
        _commandHandler = commandHandler;
        _getByIdHandler = getByIdHandler;
        _getAllHandler = getAllHandler;
    }

    /// <summary>Creates a batch of attendance exceptions.</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAttendanceExceptionsCommand command)
    {
        var ids = await _commandHandler.Handle(command);
        return Ok(new { ids });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var dto = await _getByIdHandler.Handle(new GetAttendanceExceptionByIdQuery { Id = id });
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? employeeId = null,
        [FromQuery] string? exceptionType = null,
        [FromQuery] DateOnly? from = null,
        [FromQuery] DateOnly? to = null,
        [FromQuery] AttendanceExceptionStatus? status = null)
    {
        var query = new GetAllAttendanceExceptionsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            EmployeeId = employeeId,
            ExceptionType = exceptionType,
            From = from,
            To = to,
            Status = status
        };

        var (items, total) = await _getAllHandler.Handle(query);
        return Ok(new { items, total });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateAttendanceExceptionCommand command)
    {
        if (id != command.Id) return BadRequest("Route id and payload id must match.");
        await _commandHandler.Handle(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, [FromQuery] string userId)
    {
        await _commandHandler.Handle(new DeleteAttendanceExceptionCommand { Id = id, UserId = userId });
        return NoContent();
    }

    /// <summary>Forwards the exception to the attendance cell.</summary>
    [HttpPost("{id}/submit")]
    public async Task<IActionResult> Submit(string id, [FromBody] SubmitAttendanceExceptionCommand command)
    {
        command.Id = id;
        await _commandHandler.Handle(command);
        return NoContent();
    }

    [HttpPost("{id}/approve")]
    public async Task<IActionResult> Approve(string id, [FromBody] ApproveAttendanceExceptionCommand command)
    {
        command.Id = id;
        await _commandHandler.Handle(command);
        return NoContent();
    }

    [HttpPost("{id}/reject")]
    public async Task<IActionResult> Reject(string id, [FromBody] RejectAttendanceExceptionCommand command)
    {
        command.Id = id;
        await _commandHandler.Handle(command);
        return NoContent();
    }

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> Cancel(string id, [FromBody] CancelAttendanceExceptionCommand command)
    {
        command.Id = id;
        await _commandHandler.Handle(command);
        return NoContent();
    }
}
