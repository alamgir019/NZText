using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly UserCommandHandler _handler;

    public LoginController(UserCommandHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand cmd)
    {
        var user = await _handler.Handle(cmd);
        if (user == null)
            return Unauthorized("Invalid username or password.");

        // Store user ID in session
        HttpContext.Session.SetString("UserId", user.Id);

        return Ok(new { user.Id, user.Username, user.RoleId });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("UserId");
        return Ok(new { message = "Logged out successfully." });
    }

    [HttpGet("me")]
    public IActionResult Me()
    {
        var userId = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        // Fetch user info as needed
        return Ok(new { userId });
    }

}
