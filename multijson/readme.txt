
Beskrivning:
=============================
* Detta projekt skall köras som en webbapp i IIS för att tillhandahålla access
    till databasen från webben.

Syften och mål:
===============
* Spara tid genom att kunna publicera webtjänster utan att skriva c#-kod.
* Spara tid genom att inte behöva skriva (eller konfigurera) kod som konvertera
    data mellan objekt och tabeller.

Features:
=========
* Konfigurera i web.config för att tala om
  * Vilken connectionString skall användas
  * Vilken Stored Procedure skall anropas
  * Vilken path skall ageras på
* Koda din Stored Procedure för att:
  * Tolka segmenten i URL:en för att välja hur anropet skall hanteras
  * Tolka innehållet i Request body om data skall uppdateras
  * Returnera en textsträng med Response body
  * Returnera en return code
  * Resutrnera ev. egna headers och dess värden.
* Stöd för att skicka in användare om någon form av authentication använts.
    (Authentieringen ställs in i IIS.)
* Stöd för flera olika registrerade prockar, som reagerar på olika
        path-matchningar.


TODO:
=============================
Att utveckla:
-------------
* Stöd för att skicka in headers som temptabell till registrerad procedure.
* Fallback-Handler som kan ge hjälp till utvecklare i testmiljöer.
* Validering som ger begiprliga fel istället för exceptions.
* Referensimplementation/dokumentation
* Mer kontroll över anropen
  * Lägga till {parametrar} till routeTemplate, som skickas in som parametrar till procken.
  * Lägga till constraints till Http-routen.
  * Lägga till defaults till Http-routen.

Att testa och verifiera:
-----------------------
* Säkerställ att IP-filterfunktionen inne i IIS kan användas.
* Säkerställ att basic authentication fungerar.
* Säkerställ att SSL/https fungerar.
* Utred om det är UTF-8 som kommer ut automatiskt, eller om man måste konvertera
* Att bara returnera en färdig html-sida som response verkar inte IIS:en gilla.
    Undersök vad detta beror på och vad som i så fall krävs för att det skall fungera.


Användarmanual:
===============
Work in progress...
