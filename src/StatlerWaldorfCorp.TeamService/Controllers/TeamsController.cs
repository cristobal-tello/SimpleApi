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

        [HttpGet]
        /*
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
        public IActionResult CreateTeam([FromBody] Team newTeam)
        { 
            return this.Ok(this.repository.Add(newTeam));
        }

        [HttpGet("{id}")]
        public IActionResult GetTeam(Guid id)
        {
            Team team = repository.Get(id);

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
    }
}