Spesielle notiser innlevering mappe 2:

Start-url for prosjektet: /Home/Index (Her opprettes default-administrator hvis denne ikke finnes,
samt default-produkter hvis disse ikke finnes.) Herfra kan man gå til Admin-siden som utgjør mappe 2
ved å klikke på "Admin" øverst til høyre i navbaren. Her må man logge inn med brukernavn Admin og
passord 12345678 (hoved/default-administrator). Man kan bare opprette nye administratorer
når man er logget inn som en administrator.

Mappe 2 benytter ADMINMainController. De andre controllerne er fra mappe 1. De eneste endringene som er
gjort i de andre controllerne er i HomeController, hvor vi har lagt inn metoder for å automatisk opprette
database-elementer hvis de ikke finnes.

Det skrives til en logg-fil i DAL.DBGodtSkodd, og denne logg-filen ligger under GodtSkoddProsjekt,
som er en av de 5 prosjektene, under en mappe som heter Logs (denne vises ikke i prosjekt-strukturen,
men ligger der hvis man går til filutforskeren). Det blir opprettet en ny logg-fil for
hver dag det oppstår en feil.

Vi har brukt et privat repository på GitHub til versjonskontroll, gjennom Visual Studio. Det ligger
noen github-filer i prosjektmappen som vitner om dette, men hvis du ønsker tilgang til dette
repository'et så kan vi sende deg en link og legge deg til som collaborator.

// ---------------------------------------------------------------------------------

Spesielle notiser innlevering mappe 1:

Hvis det ikke ligger noen produkt i databasen:
	Slett den aktuelle databasen.
	Start prosjektet og registrer en ny bruker (denne vil få id 1).
	Gå til Admin/Index (denne er ikke beskyttet med noe sikkerhet ennå).
	Trykk på knappen "Lag produkter" (her opprettes en ordre (og ordrelinjer) som knytter seg
		til bruker med id 1 i tillegg til å opprette alle produktene i databasen - slik kan
		man se hvordan utskriften blir under Ordrehistorikk på Min side når ordre er lagret).

Se eksempel på ordrehistorikk hvis databasen er fylt første gangen prosjektet pakkes ut:
	Hvis databasen er full av produkter når lærer kjører prosjektet første gang, skal det
	finnes en bruker med brukernavn annika som har id 1:
	Da kan man logge inn med brukernavn "annika" og passord "annikahammervoll" og gå inn på
	ordrehistorikk på Min side, hvor man vil få opp en ordre der (slik kan man se hvordan
	utskriften blir når ordre er lagret).