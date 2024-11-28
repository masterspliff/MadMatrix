using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]

// Test comment to trigger workflow       
// second test
public class TestController : ControllerBase
{
    private readonly IMongoDatabase _database;

    public TestController(IMongoDatabase database)
    {
        _database = database;
    }

    [HttpGet("connection")]
    public async Task<IActionResult> TestConnection()
    {
        try
        {
            await _database.RunCommandAsync<BsonDocument>(new BsonDocument("ping", 1));
            return Ok(new { message = "Connection to MadMatrix database successful!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Connection failed", error = ex.Message });
        }
    }
}
