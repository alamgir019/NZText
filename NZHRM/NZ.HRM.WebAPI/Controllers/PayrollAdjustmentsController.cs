using Microsoft.AspNetCore.Mvc;
using NZ.Payroll.Application.PayrollAdjustments.Commands;
using NZ.Payroll.Application.PayrollAdjustments.Handlers;
using NZ.Payroll.Application.PayrollAdjustments.Queries;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PayrollAdjustmentsController : ControllerBase
{
    private readonly PayrollAdjustmentCommandHandler _commandHandler;
    private readonly GetPayrollAdjustmentByIdQueryHandler _getByIdHandler;
    private readonly GetAllPayrollAdjustmentsQueryHandler _getAllHandler;

    public PayrollAdjustmentsController(
        PayrollAdjustmentCommandHandler commandHandler,
        GetPayrollAdjustmentByIdQueryHandler getByIdHandler,
        GetAllPayrollAdjustmentsQueryHandler getAllHandler)
    {
        _commandHandler = commandHandler;
        _getByIdHandler = getByIdHandler;
        _getAllHandler = getAllHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePayrollAdjustmentCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var id = await _commandHandler.Handle(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var dto = await _getByIdHandler.Handle(id);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? attendanceMonth = null, [FromQuery] string? companyId = null, [FromQuery] string? employeeId = null, [FromQuery] string? status = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var query = new GetAllPayrollAdjustmentsQuery
        {
            AttendanceMonth = attendanceMonth,
            CompanyId = companyId,
            EmployeeId = employeeId,
            Status = status,
            Page = page > 0 ? page : 1,
            PageSize = pageSize > 0 ? pageSize : 20
        };

        var (items, total) = await _getAllHandler.Handle(query);
        return Ok(new { items, total });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdatePayrollAdjustmentCommand command)
    {
        if (id != command.Id) return BadRequest("Id mismatch");
        await _commandHandler.Handle(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _commandHandler.HandleDelete(id);
        return NoContent();
    }
}
