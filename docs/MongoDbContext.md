# MongoDbContext Dokumentation

## Oversigt
MongoDbContext klassen er den primære database kontekst for MadMatrix applikationen. Den håndterer forbindelsen til MongoDB databasen og giver adgang til alle collections.

## Arkitektur
```mermaid
graph TD
    A[MongoDbContext] --> B[IMongoDatabase]
    A --> C[IMongoClient]
    A --> D[Collections]
    D --> E[Events]
    D --> F[Locations]
    D --> G[Users]
    D --> H[Tasks]
```

## Konfiguration
Forbindelsen til MongoDB konfigureres gennem:
1. Connection string i appsettings.json
2. Password fra miljøvariabel 'MONGODB_PASSWORD'

### Connection String Format
```
mongodb+srv://obnorup:{password}@cluster0.akttf.mongodb.net/MadMatrix
```

## Collections
MongoDbContext giver adgang til følgende collections:
- Events (TaskEvent objekter)
- Locations (Location objekter)
- Users (User objekter)
- Tasks (TaskItem objekter)

## Initialisering
Ved opstart:
1. Henter connection string fra konfiguration
2. Erstatter {password} med værdien fra miljøvariablen
3. Opretter forbindelse til MongoDB
4. Tester forbindelsen med ping
5. Logger resultatet af forbindelsen

## Fejlhåndtering
- Ved forbindelsesfejl:
  - Udskriver rød fejlmeddelelse i konsollen
  - Kaster exception videre
- Ved succesfuld forbindelse:
  - Logger bekræftelsesmeddelelse

## Sikkerhed
- Password gemmes aldrig i kildekoden
- Kræver miljøvariabel 'MONGODB_PASSWORD'
- Bruger MongoDB Atlas sikkerhedsfeatures
