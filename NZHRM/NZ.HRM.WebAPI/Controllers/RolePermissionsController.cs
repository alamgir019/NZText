using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.RolePermissions.Commands.CreateRolePermission;
using NZ.HRM.Application.RolePermissions.Commands.DeleteRolePermission;
using NZ.HRM.Application.RolePermissions.Commands.UpdateRolePermission;
using NZ.HRM.Application.RolePermissions.Handlers;
using NZ.HRM.Application.RolePermissions.Queries.GetAllRolePermissions;
using NZ.HRM.Application.RolePermissions.Queries.GetRolePermissionById;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolePermissionsController : ControllerBase
{
    private readonly RolePermissionQueryHandler _queryHandler;
    private readonly RolePermissionCommandHandler _commandHandler;

    public RolePermissionsController(RolePermissionQueryHandler queryHandler, RolePermissionCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _queryHandler.Handle(new GetAllRolePermissionsQuery());
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var item = await _queryHandler.Handle(new GetRolePermissionByIdQuery { Id = id });
        if (item == null) return NotFound(new { message = $"RolePermission with ID {id} not found" });
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRolePermissionCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var id = await _commandHandler.Handle(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateRolePermissionCommand command)
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
        var command = new DeleteRolePermissionCommand { Id = id };
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
