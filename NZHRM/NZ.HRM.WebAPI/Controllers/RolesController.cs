using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly RoleCommandHandler _commandHandler;
    private readonly RoleQueryHandler _queryHandler;

    public RolesController(RoleCommandHandler commandHandler, RoleQueryHandler queryHandler)
    {
        _commandHandler = commandHandler;
        _queryHandler = queryHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _queryHandler.Handle(new GetAllRolesQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id) =>
        Ok(await _queryHandler.Handle(new GetRoleByIdQuery(id)));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand cmd) =>
        Ok(await _commandHandler.Handle(cmd));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateRoleCommand cmd)
    {
        await _commandHandler.Handle(cmd with { Id = id });
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _commandHandler.Handle(new DeleteRoleCommand(id));
        return NoContent();
    }
}
