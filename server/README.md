# Server Documentation

## Architecture

### Repository Pattern with MongoDB
The application uses the repository pattern to abstract data access operations. Each model (Event, Location, Task, User) has:

1. An interface (e.g., `IEventRepository`) defining the contract:
```csharp
public interface IEventRepository
{
    Task<IEnumerable<Event>> GetAllAsync();
    Task<Event?> GetByIdAsync(int id);
    Task<Event> CreateAsync(Event entity);
    Task UpdateAsync(Event entity);
    Task DeleteAsync(int id);
}
```

2. A concrete implementation using MongoDB (e.g., `EventRepository`):
```csharp
public class EventRepository : IEventRepository
{
    private readonly IMongoCollection<Event> _events;

    public EventRepository(MongoDbContext context)
    {
        _events = context.Events;
    }
    // Implementation of CRUD operations
}
```

### MongoDB Integration
Data persistence is handled through MongoDB using the official MongoDB.Driver. The `MongoDbContext` class manages database connections and collections:

```csharp
public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Event> Events => _database.GetCollection<Event>("Events");
    // Other collections...
}
```

## Key Namespaces

### Microsoft.AspNetCore.Mvc
This is a fundamental namespace in ASP.NET Core that provides the building blocks for creating Web APIs. It contains:

- **ControllerBase**: Base class for API controllers
- **[ApiController]**: Attribute that designates a class as a Web API controller
- **[Route]**: Attribute for defining API routing
- **Action Results**: Types like `IActionResult`, `Ok()`, `BadRequest()`
- **HTTP Verb Attributes**: `[HttpGet]`, `[HttpPost]`, `[HttpPut]`, `[HttpDelete]`

Example usage in controllers:
```csharp
using Microsoft.AspNetCore.Mvc;  // Import the namespace

[ApiController]  // Mark as API controller
[Route("[controller]")]  // Define routing
public class UserController : ControllerBase  // Inherit from ControllerBase
{
    [HttpGet]  // HTTP verb attribute
    public IActionResult Get()  // Action result type
    {
        return Ok();  // Helper method from ControllerBase
    }
}
```

This namespace is essential for building RESTful APIs as it provides all the necessary components to handle HTTP requests and responses in a standardized way.
