Original Github repo: https://github.com/microservices-aspnetcore/teamservice

StatlerWaldorfCorpToken : e5b7411e3a67a840e091cb76537a13c22ac8dc33 (for Sonar)

* EF Code first (We need postgresql)
Create EF Code first database (StatlerWaldorfCorp.TeamService) and go to cmd:

Open a cmd and type: 

dotnet ef migrations add postgresqTeamlMigration --context TeamDbContext
dotnet ef database update --context TeamDbContext

dotnet ef migrations add postgresqlLocationMigration --context LocationDbContext
dotnet ef database update --context LocationDbContext

* SONAR
StatlerWaldorfCorpToken : e5b7411e3a67a840e091cb76537a13c22ac8dc33

Create a .bat file on .sln folder with next content:

dotnet C:\sonar-scanner-msbuild-4.5.0.1761-netcoreapp2.0\SonarScanner.MSBuild.dll begin /k:e5b7411e3a67a840e091cb76537a13c22ac8dc33
dotnet build .
dotnet C:\sonar-scanner-msbuild-4.5.0.1761-netcoreapp2.0\SonarScanner.MSBuild.dll end

* How to test API

To test API instead of cURL you can use Fiddler.

In order to test it, you can use Fiddler as client (using Composer option), "parse" tab. eg:

POST http://localhost:<port>/teams

User-Agent: Fiddler
Content-Type: application/json

Request body:
{"id": "e52baa63-d511-417e-9e54-7aab0428612", "name":"Madrid"}

* TEAM Service API

/teams GET Gets a list of all teams
/teams/{id} GET Gets details for a single team
/teams/{id}/members GET Gets members of a team
/teams POST Creates a new team
/teams/{id}/members POST Adds a member to a team
/teams/{id} PUT Updates team properties
/teams/{id}/members/{memberId} PUT Updates member properties
/teams/{id}/members/{memberId} DELETE Removes a member from the team
/teams/{id} DELETE Deletes an entire team

-> Add a Team

POST http://localhost:<port>/teams
Content-Type: application/json

Request body:
{"id": "e17c33b5-4705-459d-a597-0e47728b782a", "name":"Equipo Madrid"}
{"id": "02c420c3-291a-46ef-8133-11ab284fd44d", "name":"Equipo Barcelona"}

->  Add a member into a team
POST http://localhost:<port>/teams/e17c33b5-4705-459d-a597-0e47728b782a/members/
Content-Type: application/json
Request body:
{"id": "84ae6b52-1596-43bb-88b4-11c579b49105", "FirstName":"Luca","LastName":"Modric"}
{"id": "5ef3ccc2-8deb-4fc4-b9a2-d9865b84ad1d", "FirstName":"Karen","LastName":"Benzema"}
{"id": "fdaef2af-f563-4dbf-b476-b2dcec4b526b", "FirstName":"Gareth","LastName":"Bale"}

POST http://localhost:<port>/teams/02c420c3-291a-46ef-8133-11ab284fd44d/members/
{"id": "eb7a2327-ed73-46b6-ae0f-3136d2b28f23", "FirstName":"Gerard","LastName":"Pique"}
{"id": "459b425d-2dcf-4953-9256-c74502522eb3", "FirstName":"Sergio","LastName":"Busquets"}
{"id": "d79d26e7-7727-4e78-9230-613aa397df46", "FirstName":"Marc-Andre","LastName":"Terstegen"}

* LOCATION Service REST API

/locations/{memberID}/latest GET Retrieves the most current location of a member
/locations/{memberID} POST Adds a location record to a member
/locations/{memberID} GET Retrieves the location history of a member


-> Add location

For Modric

POST http://localhost:<port>/locations/84ae6b52-1596-43bb-88b4-11c579b49105/
Content-Type: application/json

Request body:

{"id":"4affc110-0038-45bc-95ff-61244f33e0b8","latitude":40.500,"longitude":-3.889,"altitude":718.0,"timestamp":1550242721,"memberID":"84ae6b52-1596-43bb-88b4-11c579b49105"}

For Benzema
POST http://localhost:<port>/locations/5ef3ccc2-8deb-4fc4-b9a2-d9865b84ad1d/
{"id":"4f9d5c9d-6356-4108-a09e-79ae3c26b845","latitude":40.515,"longitude":-3.642,"altitude":711.0,"timestamp":1550242769, "memberID":"5ef3ccc2-8deb-4fc4-b9a2-d9865b84ad1d"}

For Bale
POST http://localhost:<port>/locations/fdaef2af-f563-4dbf-b476-b2dcec4b526b
{"id":"a9d0c685-2a58-4b4c-8e41-4baf61a61ac2","latitude":40.470,"longitude":-3.688,"altitude":699.0,"timestamp":1550242771 ,"memberID":"fdaef2af-f563-4dbf-b476-b2dcec4b526b"}

For Pique
POST http://localhost:<port>/locations/eb7a2327-ed73-46b6-ae0f-3136d2b28f23
{"id":"345feab3-bdb9-4131-84fb-9777e276d59a","latitude":41.375,"longitude":2.079,"altitude":12.0,"timestamp":1550242775 ,"memberID":"eb7a2327-ed73-46b6-ae0f-3136d2b28f23"}

For Busquets
POST http://localhost:<port>/locations/459b425d-2dcf-4953-9256-c74502522eb3
{"id":"0a46993f-bdf2-436e-94f1-648f939dc524","latitude":41.505,"longitude":2.115,"altitude":20.0,"timestamp":1550242776 ,"memberID":"459b425d-2dcf-4953-9256-c74502522eb3"}

For Terstegen
POST http://localhost:<port>/locations/d79d26e7-7727-4e78-9230-613aa397df46
{"id":"5ce6c4bc-80b1-4dc1-9fcb-5d95b3c2727c","latitude":41.383,"longitude":2.129,"altitude":11.0,"timestamp":1550242777 ,"memberID":"d79d26e7-7727-4e78-9230-613aa397df46"}

