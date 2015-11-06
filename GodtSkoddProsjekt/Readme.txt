Spesielle notiser innlevering mappe 2:

Start-url: /Home/Index
Herfra kan man gå til Admin-siden ved å klikke på "Admin" øverst til høyre i navbaren.
Her må man logge inn med brukernavn annika og passord annikahammervoll (hoved-administrator).
Man kan bare opprette nye administratorer når man er logget inn som en administrator.

Produktene opprettes automatisk når man går inn på hovedsiden /Home/Index hvis de ikke finnes
i databasen fra før av.

Det skrives til en logg-fil i DAL.DBGodtSkodd, og denne logg-filen ligger under GodtSkoddProsjekt,
som er en av de 5 prosjektene, under en mappe som heter Logs (denne vises ikke i prosjekt-strukturen,
men ligger der hvis man går til filutforskeren). Det blir opprettet en ny logg-fil for
hver dag det oppstår en feil.

Liten notis: I UnitTest-prosjektet heter test-filen UserControllerTest.cs, men denne tester
ADMINMainControlleren, ikke UserControlleren.

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