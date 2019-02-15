using Models = StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;


namespace StatlerWaldorfCorp.TeamService.Repositories.Memory
{
    public class MemoryTeamRepository : ITeamRepository
    {
        protected ICollection<Models.Team> teams;

        public MemoryTeamRepository()
        {
            if (teams == null)
            {
                teams = new List<Models.Team>();
            }
        }

        public MemoryTeamRepository(ICollection<Models.Team> teams)
        {
            if (teams != null)
            {
                teams.Clear();
            }
            this.teams = new List<Models.Team>(teams);
        }

        public IEnumerable<Models.Team> List()
        {
            return teams;
        }

        public Models.Team Get(Guid id)
        {
            return teams.FirstOrDefault(t => t.ID == id);
        }

        public Models.Team Update(Models.Team team)
        {
            Models.Team deletedTeam = this.Delete(team.ID);

            if (deletedTeam != null)
            {
                deletedTeam = this.Add(team);
            }

            return deletedTeam;
        }

        public Models.Team Add(Models.Team team)
        {
            teams.Add(team);
            return team;
        }

        public Models.Team Delete(Guid id)
        {
            var q = teams.Where(t => t.ID == id);
            Models.Team team = null;

            if (q.Any())
            {
                team = q.First();
                teams.Remove(team);
            }

            return team;
        }
    }
}