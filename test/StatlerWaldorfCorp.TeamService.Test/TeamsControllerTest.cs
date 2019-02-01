using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Persistence;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StatlerWaldorfCorp.TeamService

{
    public class TeamsControllerTest
    {
        TeamsController controller = new TeamsController(new TestMemoryTeamRepository());

        [Fact]
        public void QueryTeamListReturnsCorrectTeams()
        {
            var rawTeams = (IEnumerable<Team>)(controller.GetAllTeams() as ObjectResult).Value;
            List<Team> teams = new List<Team>(rawTeams);
            Assert.Equal(2, teams.Count);
        }

        [Fact]
        public void CreateTeamAddsTeamToList()
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());

            var teams = (IEnumerable<Team>)(controller.GetAllTeams() as ObjectResult).Value;
            List<Team> original = new List<Team>(teams);

            Team t = new Team("sample");
            var result = controller.CreateTeam(t);

            var actionResult = controller.GetAllTeams() as ObjectResult;
            var newTeamsRaw = (IEnumerable<Team>)(controller.GetAllTeams() as ObjectResult).Value;

            List<Team> newTeams = new List<Team>(newTeamsRaw);
            Assert.Equal(newTeams.Count, original.Count + 1);

            var sampleTeam = newTeams.FirstOrDefault(target => target.Name == "one");
            Assert.NotNull(sampleTeam);
        }
    }
}