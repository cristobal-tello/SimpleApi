using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using StatlerWaldorfCorp.TeamService;
using StatlerWaldorfCorp.TeamService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;

public class SimpleIntegrationTests
{
    private readonly TestServer testServer;
    private readonly HttpClient testClient;
    private readonly Team teamZombie;
    public SimpleIntegrationTests()
    {
        testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
        testClient = testServer.CreateClient();
        teamZombie = new Team()
        {
            ID = Guid.NewGuid(),
            Name = "Zombie"
        };
    }

    
    public async void TestTeamPostAndGet()
    {
        StringContent stringContent = new StringContent(JsonConvert.SerializeObject(teamZombie), UnicodeEncoding.UTF8, "application/json");

        HttpResponseMessage postResponse =  await testClient.PostAsync( "/teams", stringContent);
        postResponse.EnsureSuccessStatusCode();

        var getResponse = await testClient.GetAsync("/teams");
        getResponse.EnsureSuccessStatusCode();

        string raw = await getResponse.Content.ReadAsStringAsync();
        List<Team> teams =  JsonConvert.DeserializeObject<List<Team>>(raw);

#pragma warning disable xUnit2013 // Do not use equality check to check for collection size.
        foreach(var t in teams)
        {
            System.Diagnostics.Trace.WriteLine(t.Name);
        }
        Assert.Equal(3, teams.Count());     // CAUTION: Set to 1 if you try to debbuging because it use MemoryTeamRepository instead MemoryTeamRepositoryTest
#pragma warning restore xUnit2013 // Do not use equality check to check for collection size.
        Assert.Equal("Zombie", teams[0].Name);
        Assert.Equal(teamZombie.ID, teams[0].ID);
    }
}