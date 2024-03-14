using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private List<User> _users = new List<User>
    {
        new User { Id = 1, Username = "user1", Password = "password1" },
        new User { Id = 2, Username = "user2", Password = "password2" },
        new User { Id = 3, Username = "user3", Password = "password3" }
    };

    [HttpPost("login")]
    public IActionResult Login([FromBody] User model)
    {
        var user = _users.SingleOrDefault(u => u.Username == model.Username && u.Password == model.Password);
        if (user == null)
            return Unauthorized("Nieprawidłowy email lub hasło");


        return Ok("Zalogowano");
    }
}
