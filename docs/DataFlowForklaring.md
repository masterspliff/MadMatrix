# Dataflow i MadMatrix Systemet

## Hvordan Fungerer Dataflowet?
I MadMatrix systemet har vi en særlig måde at håndtere data på, som sikrer at alt fungerer effektivt og sikkert. Når du som bruger interagerer med systemet, går al kommunikation gennem flere lag, der hver især har deres specifikke opgave. Det centrale i denne proces er samspillet mellem Controllers (kontrolenheder) og Repositories (datahåndtering).

## Controllers - Systemets Trafikdirigenter
Controllers fungerer som systemets trafikdirigenter. De modtager alle anmodninger fra brugerne og sender dem videre til den rette afdeling i systemet. For eksempel håndterer EventController alle anmodninger der har med events at gøre. Når en bruger vil se alle events, oprette et nyt event, eller ændre i et eksisterende event, er det EventController der styrer processen. Controllers sørger også for at give passende svar tilbage til brugeren, for eksempel en bekræftelse på at et event er blevet oprettet.

## Repositories - Systemets Dataeksperter
Repositories er specialister i at håndtere data. De ved præcis hvordan data skal gemmes og hentes fra databasen. I stedet for at hver Repository skal kende alle detaljer om databaseforbindelsen, bruger de MongoDbContext, som er en central komponent der håndterer al kommunikation med databasen. Dette gør systemet mere sikkert og lettere at vedligeholde, da alle databaseoperationer går gennem samme kanal.

## MongoDbContext - Den Centrale Forbindelse
MongoDbContext er som en oversætter mellem systemet og databasen. I stedet for at hver Repository skal oprette sin egen forbindelse til databasen, får de adgang gennem MongoDbContext. Dette er smart af flere grunde:
- Det sparer ressourcer, da vi kun behøver én aktiv forbindelse
- Det gør det lettere at ændre i databaseopsætningen, da vi kun skal ændre ét sted
- Det øger sikkerheden, da alle sikkerhedsforanstaltninger er samlet ét sted

## Praktisk Eksempel
Lad os tage et konkret eksempel: Når en bruger vil se alle events, sker følgende:
1. EventController modtager anmodningen gennem GetAll() metoden
2. Controlleren beder EventRepository om at hente alle events via GetAllAsync()
3. Repository'et bruger MongoDbContext til at kommunikere med databasen
4. Databasen sender data tilbage gennem samme vej
5. Controlleren formaterer data og sender det tilbage til brugeren

Dette system sikrer, at data håndteres ensartet og sikkert, uanset hvilken type information der er tale om.
