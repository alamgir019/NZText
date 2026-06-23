using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserCommandHandler _commandHandler;
    private readonly UserQueryHandler _queryHandler;

    public UsersController(UserCommandHandler commandHandler, UserQueryHandler queryHandler)
    {
        _commandHandler = commandHandler;
        _queryHandler = queryHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _queryHandler.Handle(new GetAllUsersQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id) =>
        Ok(await _queryHandler.Handle(new GetUserByIdQuery(id)));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand cmd) =>
        Ok(await _commandHandler.Handle(cmd));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateUserCommand cmd)
    {
        await _commandHandler.Handle(cmd with { Id = id });
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _commandHandler.Handle(new DeleteUserCommand(id));
        return NoContent();
    }
}
