using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class MenuPermissionController : ControllerBase
{
    private readonly MenuPermissionCommandHandler _handler;

    public MenuPermissionController(MenuPermissionCommandHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMenuPermissionCommand cmd)
    {
        var id = await _handler.Handle(cmd);
        return Ok(new { Id = id });
    }
}
