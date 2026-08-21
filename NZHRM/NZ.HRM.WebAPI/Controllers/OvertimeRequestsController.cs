using Microsoft.AspNetCore.Mvc;
using NZ.Attendance.Application.OvertimeRequests.Queries.GetOvertimeRequestById;
using NZ.Attendance.Application.OvertimeRequests.Commands.CreateOvertimeRequest;
using NZ.Attendance.Application.OvertimeRequests.Handlers;
using NZ.Attendance.Application.OvertimeRequests.Queries.GetAllOvertimeRequests;
using NZ.Attendance.Application.OvertimeRequests.Queries.GetEmployeesByShift;

namespace NZ.Attendance.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OvertimeRequestsController : ControllerBase
{
    private readonly OvertimeRequestCommandHandler _commandHandler;
    private readonly GetOvertimeRequestByIdQueryHandler _getByIdHandler;
    private readonly GetAllOvertimeRequestsQueryHandler _getAllHandler;
    private readonly GetEmployeesByShiftQueryHandler _getEmployeesByShiftHandler;

    public OvertimeRequestsController(
        OvertimeRequestCommandHandler commandHandler,
        GetOvertimeRequestByIdQueryHandler getByIdHandler,
        GetAllOvertimeRequestsQueryHandler getAllHandler,
        GetEmployeesByShiftQueryHandler getEmployeesByShiftHandler)
    {
        _commandHandler = commandHandler;
        _getByIdHandler = getByIdHandler;
        _getAllHandler = getAllHandler;
        _getEmployeesByShiftHandler = getEmployeesByShiftHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOvertimeRequestCommand command)
    {
        var id = await _commandHandler.Handle(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetOvertimeRequestByIdQuery { Id = id };
        var dto = await _getByIdHandler.Handle(query);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllOvertimeRequestsQuery
        {
            PageNumber = int.TryParse(HttpContext.Request.Query["pageNumber"], out var p) ? p : 1,
            PageSize = int.TryParse(HttpContext.Request.Query["pageSize"], out var s) ? s : 20,
            ShiftId = HttpContext.Request.Query["shiftId"].ToString(),
            DepartmentId = HttpContext.Request.Query["departmentId"].ToString(),
            Status = HttpContext.Request.Query["status"].ToString()
        };

        if (DateTime.TryParse(HttpContext.Request.Query["from"], out var from)) query.From = from;
        if (DateTime.TryParse(HttpContext.Request.Query["to"], out var to)) query.To = to;

        var (items, total) = await _getAllHandler.Handle(query);
        return Ok(new { items, total });
    }


    [HttpPost("approve")]
    public async Task<IActionResult> Approve([FromBody] List<Application.OvertimeRequests.Commands.ApproveOvertimeRequest.ApproveOvertimeRequestCommand> commands)
    {
        if (commands == null || !commands.Any()) return BadRequest("No commands provided");
        await _commandHandler.Handle(commands);
        return NoContent();
    }

    [HttpGet("employees-by-shift/{shiftId}")]
    public async Task<IActionResult> GetEmployeesByShift(string shiftId)
    {
        var query = new GetEmployeesByShiftQuery { ShiftId = shiftId };
        var employees = await _getEmployeesByShiftHandler.Handle(query);
        return Ok(employees);
    }
}
