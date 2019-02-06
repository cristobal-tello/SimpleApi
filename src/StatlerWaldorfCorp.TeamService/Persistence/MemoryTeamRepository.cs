using StatlerWaldorfCorp.TeamService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StatlerWaldorfCorp.TeamService.Persistence
{
    public class MemoryTeamRepository : ITeamRepository
    {
        protected ICollection<Team> teams;

        public MemoryTeamRepository()
        {
            if (teams == null)
            {
                teams = new List<Team>();
            }
        }

        public MemoryTeamRepository(ICollection<Team> teams)
        {
            if (teams != null)
            {
                teams.Clear();
            }
            this.teams = new List<Team>(teams);
        }

        public IEnumerable<Team> List()
        {
            return teams;
        }

        public Team Get(Guid id)
        {
            return teams.FirstOrDefault(t => t.ID == id);
        }

        public Team Update(Team team)
        {
            Team deletedTeam = this.Delete(team.ID);

            if (deletedTeam != null)
            {
                deletedTeam = this.Add(team);
            }

            return deletedTeam;
        }

        public Team Add(Team team)
        {
            teams.Add(team);
            return team;
        }

        public Team Delete(Guid id)
        {
            var q = teams.Where(t => t.ID == id);
            Team team = null;

            if (q.Any())
            {
                team = q.First();
                teams.Remove(team);
            }

            return team;
        }
    }
}