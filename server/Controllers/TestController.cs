using Microsoft.AspNetCore.Mvc;
using server.Data;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly MongoDbContext _context;

    public TestController(MongoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult TestConnection()
    {
        return Ok(new { message = "Connected to MongoDB!" });
    }
}
