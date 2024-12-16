# MadMatrix MongoDB Data Flow

```mermaid
sequenceDiagram
    participant Client as HTTP Client
    participant Controller as EventController
    participant Repository as EventRepository
    participant Context as MongoDbContext
    participant Atlas as MongoDB Atlas

    Client->>+Controller: HTTP Request (GET /Event)
    Controller->>+Repository: GetAllAsync()
    Repository->>+Context: _events.Find()
    Context->>+Atlas: Execute Query
    Atlas-->>-Context: BSON Documents
    Context-->>-Repository: TaskEvent[]
    Repository-->>-Controller: IEnumerable<TaskEvent>
    Controller-->>-Client: JSON Response

    Note over Client,Atlas: Eksempel på Create Flow
    Client->>+Controller: POST /Event (TaskEvent)
    Controller->>+Repository: CreateAsync(event)
    Repository->>Repository: Generate Next ID
    Repository->>+Context: InsertOneAsync()
    Context->>+Atlas: Insert Document
    Atlas-->>-Context: Confirmation
    Context-->>-Repository: Success
    Repository-->>-Controller: Created TaskEvent
    Controller-->>-Client: 201 Created
```

## Detaljeret Flow Beskrivelse

### HTTP Request Flow
1. **Client → Controller**
   - HTTP request modtages af ASP.NET Core
   - Route matching til EventController
   - Model binding af request data

2. **Controller → Repository**
   - Controller validerer input
   - Kalder relevant repository metode
   - Håndterer HTTP status koder

### Database Operations
3. **Repository → MongoDbContext**
   - Repository bygger MongoDB queries
   - Håndterer ID generering
   - Udfører CRUD operationer via context

4. **MongoDbContext → Atlas**
   - Opretholder database forbindelse
   - Konverterer mellem C# og BSON
   - Håndterer connection pooling

### Response Flow
5. **Atlas → Client**
   - Data transformeres gennem lagene
   - Fejlhåndtering på hvert niveau
   - Konvertering til JSON response

## Sikkerhed og Performance
- Alle database kald er asynkrone
- Connection string beskyttet med miljøvariabel
- Automatisk connection pooling
- Retry logic ved netværksfejl
