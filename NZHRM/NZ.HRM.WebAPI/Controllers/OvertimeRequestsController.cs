using Microsoft.AspNetCore.Mvc;
using NZ.Attendance.Application.OvertimeRequests.Commands.CreateOvertimeRequest;
using NZ.Attendance.Application.OvertimeRequests.Handlers;
using NZ.Attendance.Application.OvertimeRequests.Queries.GetAllOvertimeRequests;
using NZ.Attendance.Application.OvertimeRequests.Queries.GetEmployeesByShift;
using NZ.Attendance.Application.OvertimeRequests.Queries.GetOvertimeRequestById;
using NZ.HRM.Utility;
using NZ.HRM.Utility.Enum;

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
    public async Task<IActionResult> GetAll(
        [FromQuery] string? shiftId = null,
        [FromQuery] string? divisionId = null,
        [FromQuery] string? unitId = null,
        [FromQuery] string? departmentId = null,
        [FromQuery] string? status = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = new GetAllOvertimeRequestsQuery
        {
            PageNumber = pageNumber > 0 ? pageNumber : 1,
            PageSize = pageSize > 0 ? pageSize : 2000,
            ShiftId = shiftId,
            DepartmentId = departmentId,
            UnitId = unitId,
            Status = status
        };

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

    [HttpGet("employees/shift/{shiftId}/department/{departmentId}")]
    public async Task<IActionResult> GetEmployeesByShift(string shiftId, string departmentId)
    {
        var query = new GetEmployeesByShiftAndDepartmentQuery { ShiftId = shiftId, DepartmentId = departmentId };
        var employees = await _getEmployeesByShiftHandler.Handle(query);
        return Ok(employees);
    }
}
