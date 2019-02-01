Original Github repo: https://github.com/microservices-aspnetcore/teamservice


To test API instead of cURL you can use Fiddler.

In order to test it, you can use Fiddler as client (using Composer option), "parse" tab. eg:

POST http://localhost:50138/teams

User-Agent: Fiddler
Content-Type: application/json

Request body:
{"id": "e52baa63-d511-417e-9e54-7aab0428612", "name":"Madrid"}
