# Dokumentation i Kode

## Indledning
Denne interne note omhandler dokumentation af kode – mere specifikt den dokumentation der skrives i koden. Eksempler i noten er taget fra .NET med C#, men kan fint overføres til andre objekt-orienterede sprog og udviklingsmiljøer som .NET.

*Ole Eriksen, December 2024*

## 1. Navngivning
Grundlæggende er dokumentationen kommentarer i koden og formålet med disse er at gøre det lettere og mere sikkert at læse, forstå og redigere kode. Et stort skridt i den retning, er helt klart at være omhyggelig med sin navngivning. Det er vigtigt at specielt instans variable/properties i klasser og metoder har gode, sigende og præcise navne.

**Så derfor er regel 1:**
> Vær omhyggelig med navngivning af variable, metoder og deres parametre.

Husk, at jo større scope en variable, metode eller klasse har, jo vigtigere er det at navnet på den er sigende. Så hvis I har f.eks. en simpel for-løkke på 8 linjer er det helt ok at kalde en tæller-variablen for i, da den kun kan bruges i de 8 linjer.

## 2. Instans-variable
Regel nummer 2 er, at vi skal beskrive betydning af instansvariable i klasser. Disse bruges i hele klassen og det er derfor vigtigt at vide hvilken rolle en given instansvariable spiller i en klasse, hvis man skal forstå logikken i hvordan klassen er implementeret. Dette gælder også properties.

Selve beskrivelsen kan placeres tæt på selve erklæringen. Et eksempel kan ses herunder.

Når I skriver dokumentationen, så tænk på hvad en udefrakommende programmør har brug for at vide, hvis vedkommende fik til opgave at ændre eller udbygge jeres program.

**Så regel nummer 2 er:**
> Dokumentér alle instans-variable

## 3. Funktionel Specifikation
Sidste regel handler om funktioner eller metoder. Vi vil bruge ordet funktion for begge i det efterfølgende. En funktion har en signatur, som er en beskrivelse af tre egenskaber ved funktionen, som er vigtig for dem som anvender eller kalder funktionen. 

De tre egenskaber er:
* Hvad returnerer funktionen?
* Hvad hedder funktionen?
* Hvilke parametre har funktionen?

Disse tre egenskaber dokumenteres til et vist niveau i selve koden, når vi skriver C#. Så for at kunne anvende en funktion, skal man vide hvad funktionen returnerer og hvordan det objekt den returnerer afhænger af parametre og/eller tilstand i klassen.

Dette skal beskrives i dokumentationen af funktionen. Her skal man derfor beskrive HVAD funktionen gør og herunder skal man beskrive hvilken betydning hver enkelt parameter har. Dette kalder vi for en funktionel specifikation. 

Når I skriver dem så husk:
* Angiv hvad funktionen returnerer - også under meget specielle eller sjældne omstændigheder
* Angiv betydning af hver parameter

Når de klasser man koder implementerer interfaces, er det en god idé at dokumentere interfacet - fremfor klassen. Overvej hvorfor det er rigtigt.

Læg mærke til at funktionen GetAll() ikke er dokumenteret. Dette er ikke nødvendigt da navngivningen er god og funktionen uhyre simpel.

**Så regel nummer 3 er:**
> Dokumenter funktioner med funktionelle specifikationer.

Det var de tre regler for minimal eller tilstrækkelig dokumentation i koden.

## 4. Værktøjer
Der findes værktøjer som udtrækker funktionelle specifikationer i koden som tekst. Dette kan I få udbytte af i forbindelse med jeres API'er. Metoderne i controllerne dokumenteres i Swagger som OpenAPI. Her skriver man de funktionelle specifikationer på en bestemt måde og så kommer dokumentationen ud på siden helt automatisk. Det er smart fordi man undgår redundans og kedelige arbejdsgange.

Her kan man se at dokumentationen er opbygget vha. nogle XML tags (summary, param, returns osv). På Swagger siden ser det derfor således ud.

## 5. Opsummering
Der er tre regler for tilstrækkelig dokumentation i og af kode:

1. Vær omhyggelig med navngivning
2. Dokumenter instans-variable i klasser
3. Dokumenter funktioner med funktionelle specifikationer
