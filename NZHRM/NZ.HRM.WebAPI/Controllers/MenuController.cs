// C#
using Microsoft.AspNetCore.Mvc;
using MediatR;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly MenuCommandHandler _commandHandler;
    private readonly MenuQueryHandler _queryHandler;

    public MenuController(MenuCommandHandler commandHandler, MenuQueryHandler queryHandler)
    {
        _commandHandler = commandHandler;
        _queryHandler = queryHandler;
    }

    //[HttpGet("{id}")]
    //public async Task<IActionResult> Get(string id) =>
    //    Ok(await _queryHandler.Handle(new GetMenuByIdQuery(id)));

    //[HttpGet]
    //public async Task<IActionResult> GetAll()
    //    => Ok(await _queryHandler.Handle(new GetAllMenusQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMenuCommand command)
        => Ok(await _commandHandler.Handle(command));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateMenuCommand command)
    {
        if (id != command.Id) return BadRequest();
        await _commandHandler.Handle(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _commandHandler.Handle(new DeleteMenuCommand(id));
        return NoContent();
    }
}
