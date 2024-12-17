using Microsoft.AspNetCore.Mvc;
using core.Models;
using server.Repositories.Login;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    /// <summary>
    /// Repository for handling user authentication
    /// </summary>
    private readonly ILoginRepository _loginRepository;

    public LoginController(ILoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    /// <summary>
    /// Authenticates a user with their credentials
    /// </summary>
    /// <param name="loginRequest">The login credentials containing email and password</param>
    /// <returns>The authenticated user if successful, or Unauthorized if credentials are invalid</returns>
    [HttpPost]
    public IActionResult Login([FromBody] LoginDto loginRequest)
    {
        Console.WriteLine($"Login attempt for email: {loginRequest.Email}");
        
        var user = _loginRepository.ValidateUser(loginRequest);
        if (user != null)
        {
            Console.WriteLine("Login successful");
            return Ok(user);
        }
        
        Console.WriteLine("Login failed - invalid credentials");
        return Unauthorized(new { message = "Invalid credentials" });
    }
}
