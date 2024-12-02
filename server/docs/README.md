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

# Repository Pattern Structure

## Overview
The repository pattern is implemented to provide a clean separation between the data access logic and the business logic of the application. Each entity (Event, Location, Task, User) has its own repository, following a consistent pattern.

## Structure
```
Repositories/
├── Event/
│   ├── IEventRepository.cs    // Interface defining Event operations
│   └── EventRepository.cs     // Concrete MongoDB implementation
├── Location/
│   ├── ILocationRepository.cs // Interface defining Location operations
│   └── LocationRepository.cs  // Concrete MongoDB implementation
├── TaskItem/
│   ├── ITaskRepository.cs     // Interface defining Task operations
│   └── TaskRepository.cs      // Concrete MongoDB implementation
└── User/
    ├── IUserRepository.cs     // Interface defining User operations
    └── UserRepository.cs      // Concrete MongoDB implementation
```

## Why This Structure?

1. **Separation of Concerns**
   - Interfaces (I*Repository.cs) define WHAT operations are available
   - Concrete implementations (*Repository.cs) define HOW these operations are performed
   - This separation allows us to change the database implementation without affecting the rest of the application

2. **Consistent Pattern**
   Each repository provides five standard operations:
   - `GetAllAsync()` - Retrieve all entities
   - `GetByIdAsync(int id)` - Retrieve a single entity by ID
   - `CreateAsync(Entity entity)` - Create a new entity
   - `UpdateAsync(Entity entity)` - Update an existing entity
   - `DeleteAsync(int id)` - Delete an entity by ID

3. **MongoDB Integration**
   - Each repository uses MongoDB.Driver for database operations
   - Repositories inject MongoDbContext to access their respective collections
   - Operations are async to ensure better performance

4. **Dependency Injection Ready**
   - Interfaces allow for easy dependency injection
   - Makes unit testing easier by allowing mock repositories
   - Follows SOLID principles, particularly the Dependency Inversion Principle

## Example Usage

```csharp
// In a controller or service:
public class EventController
{
    private readonly IEventRepository _eventRepository;

    public EventController(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Event> GetEvent(int id)
    {
        return await _eventRepository.GetByIdAsync(id);
    }
}
```

This structure ensures:
- Consistent data access patterns across the application
- Easy maintenance and testing
- Flexibility to change database implementations
- Clear separation between data access and business logic

## Controller-Repository Relationship

The application follows a common web API architecture pattern where Controllers and Repositories have distinct responsibilities:

### Controllers (Controllers/)
- Handle HTTP requests (GET, POST, PUT, DELETE)
- Perform input validation
- Define API endpoints through routing
- Call appropriate repository methods
- Return HTTP responses

### Repositories (Repositories/)
- Handle data access logic
- Perform database operations
- Abstract database implementation details

### Typical Request Flow
```
HTTP Request → Controller → Repository → Database
                ↓             ↓
            Handles URLs    Handles Data
            API Routes     DB Operations
```

For example, when handling an API request:
```
GET /api/events/123

↓ EventController handles the route and calls repository
public async Task<IActionResult> GetEvent(int id)
{
    var event = await _eventRepository.GetByIdAsync(id);
    return Ok(event);
}

↓ EventRepository handles the database operation
public async Task<Event?> GetByIdAsync(int id)
{
    var filter = Builders<Event>.Filter.Eq(e => e.Id, id);
    return await _events.Find(filter).FirstOrDefaultAsync();
}
```

This separation ensures:
- Clean separation of concerns
- Testable components
- Maintainable codebase
- Scalable architecture
