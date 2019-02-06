Original Github repo: https://github.com/microservices-aspnetcore/teamservice

StatlerWaldorfCorpToken : e5b7411e3a67a840e091cb76537a13c22ac8dc33

Place in .sln folder

dotnet C:\sonar-scanner-msbuild-4.5.0.1761-netcoreapp2.0\SonarScanner.MSBuild.dll begin /k:e5b7411e3a67a840e091cb76537a13c22ac8dc33
dotnet build .
dotnet C:\sonar-scanner-msbuild-4.5.0.1761-netcoreapp2.0\SonarScanner.MSBuild.dll end

To test API instead of cURL you can use Fiddler.

In order to test it, you can use Fiddler as client (using Composer option), "parse" tab. eg:

POST http://localhost:50138/teams

User-Agent: Fiddler
Content-Type: application/json

Request body:
{"id": "e52baa63-d511-417e-9e54-7aab0428612", "name":"Madrid"}


TEAM Service API
/teams GET Gets a list of all teams
/teams/{id} GET Gets details for a single team
/teams/{id}/members GET Gets members of a team
/teams POST Creates a new team
/teams/{id}/members POST Adds a member to a team
/teams/{id} PUT Updates team properties
/teams/{id}/members/{memberId} PUT Updates member properties
/teams/{id}/members/{memberId} DELETE Removes a member from the team
/teams/{id} DELETE Deletes an entire team

Samples

* Add a Team

POST http://localhost:50138/teams
Content-Type: application/json

Request body:
{"id": "2c9ffaab-092a-4264-b7c9-51ecaa23accd", "name":"Real Madrid"}

* Add a member into a team
POST http://localhost:50138/teams/2c9ffaab-092a-4264-b7c9-51ecaa23accd/members/
Content-Type: application/json
Request body:
{"id": "93809109-a44a-4279-8828-55febd412be6", "FirstName":"Luca","LastName":"Modric"}

{"id": "93809109-a44a-4279-8828-55febd412be7", "FirstName":"Karen","LastName":"Benzema"}
{"id": "93809109-a44a-4279-8828-55febd412be8", "FirstName":"Gareth","LastName":"Bale"}

LOCAITON Service REST API
Resource Method Description
/locations/{memberID}/latest GET Retrieves the most current location of a member
/locations/{memberID} POST Adds a location record to a member
/locations/{memberID} GET Retrieves the location history of a member


* Add location

POST http://localhost:50138/locations/93809109-a44a-4279-8828-55febd412be6/
Content-Type: application/json
Request body:
{"id":"55bf35ba-deb7-4708-abc2-a21054dbfa13","latitude":12.56,"longitude":45.567,"altitude":1200.0,"timestamp":1476029596,"memberID":"93809109-a44a-4279-8828-55febd412be6"}