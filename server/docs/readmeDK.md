# Server Dokumentation (Dansk)

## Overordnet Arkitektur
Serveren er bygget som et Web API med en lagdelt arkitektur, der følger "Repository Pattern" designmønsteret. Dette sikrer en klar adskillelse mellem forskellige ansvarområder i koden.

## Hovedkomponenter

### 1. Controllers (Kontrollere)
Kontrollerne fungerer som indgangspunkt for alle HTTP-forespørgsler og håndterer:
- HTTP requests (GET, POST, PUT, DELETE)
- Input validering
- Routing (API endpoints)
- Kalder de relevante repository-metoder
- Returnerer HTTP-svar

Eksempel på controller:
```csharp
[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventRepository _eventRepository;
    
    // Constructor med dependency injection
    public EventController(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await _eventRepository.GetAllAsync();
        return Ok(events);
    }
}
```

### 2. Repositories (Datahåndtering)
Repository-laget håndterer al kommunikation med databasen. Hver entitet (Event, Location, Task, User) har sit eget repository med følgende struktur:

#### Interface (Kontrakt)
Definerer hvilke operationer der er tilgængelige:
- GetAllAsync() - Hent alle elementer
- GetByIdAsync(int id) - Hent ét element via ID
- CreateAsync(Entity entity) - Opret nyt element
- UpdateAsync(Entity entity) - Opdater eksisterende element
- DeleteAsync(int id) - Slet element

#### Implementering
Den konkrete implementering af interfacet, som bruger MongoDB:
```csharp
public class EventRepository : IEventRepository
{
    private readonly IMongoCollection<Event> _events;

    public EventRepository(MongoDbContext context)
    {
        _events = context.Events;
    }
    // Implementation af CRUD-operationer
}
```

### 3. Database Integration (MongoDB)
Al data gemmes i MongoDB, som er en NoSQL-database. Forbindelsen håndteres gennem `MongoDbContext`:

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
    // Andre collections...
}
```

## Dataflow
1. Klient sender HTTP request (f.eks. GET /api/events)
2. Request rammer den relevante controller
3. Controller kalder repository
4. Repository udfører database-operation
5. Data sendes tilbage gennem lagene
6. Controller formaterer og returnerer HTTP response

## Fordele ved denne Arkitektur

1. **Separation of Concerns (Adskillelse af Ansvar)**
   - Hver del af systemet har ét specifikt ansvar
   - Lettere at vedligeholde og teste
   - Nemmere at forstå for nye udviklere

2. **Dependency Injection**
   - Løs kobling mellem komponenter
   - Nemmere at teste (kan mocke repositories)
   - Mere fleksibel kodebase

3. **Repository Pattern**
   - Abstraherer database-operationer
   - Konsistent måde at tilgå data
   - Kan skifte database-implementering uden at ændre resten af koden

## Sådan Tilføjer Du Ny Funktionalitet

### 1. Tilføj Ny Model
Først oprettes en model i `core/Models`:
```csharp
public class MinNyeModel
{
    public int Id { get; set; }
    public string Navn { get; set; }
    // Andre properties...
}
```

### 2. Opret Repository Interface
I `Repositories` mappen:
```csharp
public interface IMinNyeModelRepository
{
    Task<IEnumerable<MinNyeModel>> GetAllAsync();
    // Andre metoder...
}
```

### 3. Implementer Repository
```csharp
public class MinNyeModelRepository : IMinNyeModelRepository
{
    private readonly IMongoCollection<MinNyeModel> _collection;

    public MinNyeModelRepository(MongoDbContext context)
    {
        _collection = context.GetCollection<MinNyeModel>("MinNyeModel");
    }
    // Implementer metoder...
}
```

### 4. Opret Controller
```csharp
[ApiController]
[Route("[controller]")]
public class MinNyeModelController : ControllerBase
{
    private readonly IMinNyeModelRepository _repository;

    public MinNyeModelController(IMinNyeModelRepository repository)
    {
        _repository = repository;
    }
    // Implementer endpoints...
}
```

## Fejlfinding

### Almindelige Fejl
1. **404 Not Found**
   - Tjek om din route er korrekt
   - Verificer at controlleren er registreret

2. **500 Internal Server Error**
   - Tjek MongoDB forbindelse
   - Se efter exceptions i logs

3. **Null Reference Exceptions**
   - Tjek om dependency injection er sat op korrekt
   - Verificer at MongoDB collections eksisterer

### Debugging Tips
1. Brug logging aktivt
2. Tjek MongoDB connection string
3. Verificer at alle services er registreret i Program.cs

## Best Practices

1. **Navngivning**
   - Brug beskrivende navne
   - Følg C# konventioner
   - Hold navngivning konsistent

2. **Async/Await**
   - Brug altid async/await ved database-operationer
   - Undgå at blokere tråde

3. **Validering**
   - Valider input i controllers
   - Brug ModelState validering
   - Implementer business rules i services

4. **Error Handling**
   - Brug try-catch blocks strategisk
   - Returner passende HTTP status koder
   - Log fejl med tilstrækkelig kontekst

## Deployment

1. **Forberedelse**
   - Opdater connection strings
   - Tjek environment variables
   - Verificer MongoDB adgang

2. **Deployment Steps**
   - Build projektet
   - Kør tests
   - Deploy til server
   - Verificer health checks

## Vedligeholdelse

1. **Daglig Drift**
   - Monitor logs
   - Tjek performance metrics
   - Backup af data

2. **Opdateringer**
   - Hold NuGet packages opdateret
   - Test grundigt efter opdateringer
   - Hold dokumentation opdateret

## Support
Ved problemer:
1. Tjek logs
2. Verificer MongoDB status
3. Tjek network connectivity
4. Kontakt team lead hvis problemet persisterer
