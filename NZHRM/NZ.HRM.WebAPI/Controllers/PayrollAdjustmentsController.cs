using Microsoft.AspNetCore.Mvc;
using NZ.Payroll.Application.PayrollAdjustments.Commands;
using NZ.Payroll.Application.PayrollAdjustments.Handlers;
using NZ.Payroll.Application.PayrollAdjustments.Queries;
using NZ.Payroll.Application.PayrollExceptions.Commands;
using NZ.Payroll.Application.PayrollExceptions.Handlers;
using NZ.Payroll.Application.PayrollExceptions.Queries;
using System.Security.Claims;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PayrollAdjustmentsController : ControllerBase
{
    private readonly PayrollAdjustmentCommandHandler _commandHandler;
    private readonly GetAllPayrollAdjustmentsQueryHandler _getAllHandler;
    private readonly GetPayrollExceptionRequestDetailQueryHandler _getPayrollExceptionDetailHandler;
    private readonly GetPayrollExceptionRequestsQueryHandler _getPayrollExceptionsHandler;
    private readonly ForwardPayrollExceptionRequestCommandHandler _forwardPayrollExceptionHandler;
    private readonly ForwardPayrollExceptionRequestsCommandHandler _forwardPayrollExceptionsHandler;

    public PayrollAdjustmentsController(
        PayrollAdjustmentCommandHandler commandHandler,
        GetAllPayrollAdjustmentsQueryHandler getAllHandler,
        GetPayrollExceptionRequestDetailQueryHandler getPayrollExceptionDetailHandler,
        GetPayrollExceptionRequestsQueryHandler getPayrollExceptionsHandler,
        ForwardPayrollExceptionRequestCommandHandler forwardPayrollExceptionHandler,
        ForwardPayrollExceptionRequestsCommandHandler forwardPayrollExceptionsHandler)
    {
        _commandHandler = commandHandler;
        _getAllHandler = getAllHandler;
        _getPayrollExceptionDetailHandler = getPayrollExceptionDetailHandler;
        _getPayrollExceptionsHandler = getPayrollExceptionsHandler;
        _forwardPayrollExceptionHandler = forwardPayrollExceptionHandler;
        _forwardPayrollExceptionsHandler = forwardPayrollExceptionsHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePayrollAdjustmentCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var id = await _commandHandler.Handle(command);
        return Created($"/api/payroll-adjustments/{id}", new { id });
    }

    [HttpGet("{requestId}")]
    public async Task<IActionResult> GetById(string requestId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(requestId))
        {
            return BadRequest(new
            {
                code = "INVALID_REQUEST",
                message = "Payroll adjustment request identifier is required."
            });
        }

        var dto = await _getPayrollExceptionDetailHandler.Handle(new GetPayrollExceptionRequestDetailQuery { RequestId = requestId }, cancellationToken);
        if (dto == null)
        {
            return NotFound(new
            {
                code = "REQUEST_NOT_FOUND",
                message = "Payroll adjustment request was not found."
            });
        }

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

    [HttpGet("exceptions")]
    public async Task<IActionResult> GetPayrollExceptionRequests(
        [FromQuery] string? requestId = null,
        [FromQuery] string? employeeId = null,
        [FromQuery] string? employeeName = null,
        [FromQuery] string? department = null,
        [FromQuery] string? adjustmentType = null,
        [FromQuery] string? status = null,
        [FromQuery] DateOnly? attendanceDateFrom = null,
        [FromQuery] DateOnly? attendanceDateTo = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetPayrollExceptionRequestsQuery
        {
            RequestId = requestId,
            EmployeeId = employeeId,
            EmployeeName = employeeName,
            Department = department,
            AdjustmentType = adjustmentType,
            Status = status,
            AttendanceDateFrom = attendanceDateFrom,
            AttendanceDateTo = attendanceDateTo,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var response = await _getPayrollExceptionsHandler.Handle(query, cancellationToken);
        return Ok(response);
    }

    [HttpPut("forward-to-it")]
    public async Task<IActionResult> ForwardToHeadOfficeIT([FromBody] ForwardPayrollExceptionRequestsCommand command, CancellationToken cancellationToken = default)
    {
        command.ForwardedBy = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue(ClaimTypes.Name)
            ?? "SYSTEM";

        var result = await _forwardPayrollExceptionsHandler.Handle(command, cancellationToken);
        if (!result.Success)
        {
            var statusCode = result.ErrorCode switch
            {
                "REQUEST_NOT_FOUND" => StatusCodes.Status404NotFound,
                "REQUEST_ALREADY_FORWARDED" => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status400BadRequest
            };

            return StatusCode(statusCode, new
            {
                code = result.ErrorCode,
                message = result.Message
            });
        }

        return Ok(new
        {
            success = true,
            message = result.Message,
            forwardedCount = result.ForwardedCount,
            forwardedOn = result.ForwardedOn
        });
    }

    [HttpPut("{requestId}/forward-to-it")]
    public async Task<IActionResult> ForwardToHeadOfficeIT(string requestId, [FromBody] ForwardPayrollExceptionRequestCommand command, CancellationToken cancellationToken = default)
    {
        command.RequestId = requestId;
        command.ForwardedBy = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue(ClaimTypes.Name)
            ?? "SYSTEM";

        var result = await _forwardPayrollExceptionHandler.Handle(command, cancellationToken);
        if (!result.Success)
        {
            var statusCode = result.ErrorCode switch
            {
                "REQUEST_NOT_FOUND" => StatusCodes.Status404NotFound,
                "REQUEST_ALREADY_FORWARDED" => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status400BadRequest
            };

            return StatusCode(statusCode, new
            {
                code = result.ErrorCode,
                message = result.Message
            });
        }

        return Ok(new
        {
            success = true,
            message = result.Message,
            forwardedCount = result.ForwardedCount,
            forwardedOn = result.ForwardedOn
        });
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
