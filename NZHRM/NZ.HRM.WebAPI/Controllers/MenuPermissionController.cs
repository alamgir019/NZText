using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class MenuPermissionController : ControllerBase
{
    private readonly MenuPermissionCommandHandler _commandHandler;
    private readonly MenuPermissionQueryHandler _queryHandler;

    public MenuPermissionController(MenuPermissionCommandHandler commandHandler, MenuPermissionQueryHandler queryHandler)
    {
        _commandHandler = commandHandler;
        _queryHandler = queryHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMenuPermissionCommand cmd)
    {
        var id = await _commandHandler.Handle(cmd);
        return Ok(new { Id = id });
    }


    [HttpGet("menus/{userId}")]
    public async Task<IActionResult> GetMenusByUserId(string userId)
    {
        var menus = await _queryHandler.GetMenusByUserIdAsync(userId);
        return Ok(menus);
    }
}
