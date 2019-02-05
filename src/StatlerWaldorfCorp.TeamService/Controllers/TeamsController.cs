using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Persistence;
using System;

namespace StatlerWaldorfCorp.TeamService
{
    [Route("[controller]")]
    public class TeamsController : Controller
    {
        ITeamRepository repository;

        public TeamsController(ITeamRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet] /*
        public IEnumerable<Team> GetAllTeams()
		{
            // Return 2 only to pass test
            return new Team[] { new Team("one"), new Team("two") };
		}*/
        public virtual IActionResult GetAllTeams()
        {
            return this.Ok(this.repository.List());
        }

        [HttpPost]
        public virtual IActionResult CreateTeam([FromBody]Team newTeam)
        {
            this.repository.Add(newTeam);

            //TODO: add test that asserts result is a 201 pointing to URL of the created team.
            //TODO: teams need IDs
            //TODO: return created at route to point to team details			
            return this.Created($"/teams/{newTeam.ID}", newTeam);
        }


        [HttpGet("{id}")]
        public IActionResult GetTeam(Guid id)
        {
            Team team = this.repository.Get(id);

            // TO-DO: Cristobal, check new ?? operator
            if (team != null) // I HATE NULLS, MUST FIXERATE THIS.			  
            {
                return this.Ok(team);
            }
            else
            {
                return this.NotFound();
            }
        }

      

        [HttpPut("{id}")]
        public virtual IActionResult UpdateTeam([FromBody]Team team, Guid id)
        {
            team.ID = id;

            if (this.repository.Update(team) == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(team);
            }
        }

        [HttpDelete("{id}")]
        public virtual IActionResult DeleteTeam(Guid id)
        {
            Team team = this.repository.Delete(id);

            if (team == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(team.ID);
            }
        }
    }
}