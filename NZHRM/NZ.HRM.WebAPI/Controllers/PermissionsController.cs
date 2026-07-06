using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Permissions.Commands.CreatePermission;
using NZ.HRM.Application.Permissions.Commands.DeletePermission;
using NZ.HRM.Application.Permissions.Commands.UpdatePermission;
using NZ.HRM.Application.Permissions.Handlers;
using NZ.HRM.Application.Permissions.Queries.GetAllPermissions;
using NZ.HRM.Application.Permissions.Queries.GetSecPermissionById;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionsController : ControllerBase
{
    private readonly PermissionQueryHandler _queryHandler;
    private readonly PermissionCommandHandler _commandHandler;

    public PermissionsController(PermissionQueryHandler queryHandler, PermissionCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _queryHandler.Handle(new GetAllPermissionsQuery());
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var item = await _queryHandler.Handle(new GetPermissionByIdQuery { Id = id });
        if (item == null) return NotFound(new { message = $"Permission with ID {id} not found" });
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePermissionCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var id = await _commandHandler.Handle(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdatePermissionCommand command)
    {
        if (id != command.Id) return BadRequest(new { message = "ID mismatch" });
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            await _commandHandler.Handle(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeletePermissionCommand { Id = id };
        try
        {
            await _commandHandler.Handle(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
