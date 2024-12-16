# MongoDB Opsætningsguide

## Forudsætninger
- .NET 6.0 eller nyere
- Adgang til MongoDB Atlas cluster
- Miljøvariabel 'MONGODB_PASSWORD' sat

## Installation og Opsætning

### 1. Miljøvariabel Opsætning
```powershell
setx MONGODB_PASSWORD "dit-password-her"
```

### 2. NuGet Pakker
Følgende NuGet pakker skal være installeret:
- MongoDB.Driver
- MongoDB.Bson

### 3. Konfiguration
I appsettings.json skal følgende være konfigureret:
```json
{
  "ConnectionStrings": {
    "MongoDB": "mongodb+srv://obnorup:{password}@cluster0.akttf.mongodb.net/MadMatrix?retryWrites=true&w=majority"
  }
}
```

## Fejlfinding

### Almindelige Fejl

1. "Unable to connect to MongoDB"
   - Tjek at password er korrekt i miljøvariablen
   - Verificer netværksforbindelse
   - Kontroller at IP-adresse er whitelisted i MongoDB Atlas

2. "Authentication failed"
   - Gennemgå miljøvariabel opsætning
   - Verificer brugerrettigheder i MongoDB Atlas

3. "Collection not found"
   - Kontroller at collection navnet er korrekt
   - Verificer database rettigheder

## Bedste Praksis
1. Brug altid asynkrone metoder
2. Implementer retry logic ved forbindelsesfejl
3. Log alle database operationer
4. Brug indexes for optimeret performance
