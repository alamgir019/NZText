using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.UserRoles.Commands.DeleteUserRole;
using NZ.HRM.Application.UserRoles.Handlers;
using NZ.HRM.Application.UserRoles.Queries.GetAllUserRoles;
using NZ.HRM.Application.SecUserRoles.Queries.GetUserRoleById;
using NZ.HRM.Application.SecUserRoles.Commands.CreateUserRole;
using NZ.HRM.Application.SecUserRoles.Commands.UpdateUserRole;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserRolesController : ControllerBase
{
    private readonly UserRoleQueryHandler _queryHandler;
    private readonly UserRoleCommandHandler _commandHandler;

    public UserRolesController(UserRoleQueryHandler queryHandler, UserRoleCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _queryHandler.Handle(new GetAllUserRolesQuery());
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var item = await _queryHandler.Handle(new GetUserRoleByIdQuery { Id = id });
        if (item == null) return NotFound(new { message = $"UserRole with ID {id} not found" });
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRoleCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var id = await _commandHandler.Handle(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateUserRoleCommand command)
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
        var command = new DeleteUserRoleCommand { Id = id };
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
