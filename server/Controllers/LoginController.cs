using Microsoft.AspNetCore.Mvc;
using core.Models;
using server.Repositories.Login;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILoginRepository _loginRepository;

    public LoginController(ILoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    [HttpPost]
    public IActionResult Login([FromBody] LoginDto loginRequest)
    {
        var user = _loginRepository.ValidateUser(loginRequest);
        if (user != null)
        {
            return Ok(user);
        }
        
        return Unauthorized(new { message = "Invalid credentials" });
    }
}
