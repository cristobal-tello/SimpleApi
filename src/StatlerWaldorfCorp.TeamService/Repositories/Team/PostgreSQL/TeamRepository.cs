using Microsoft.EntityFrameworkCore;
using Models = StatlerWaldorfCorp.TeamService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StatlerWaldorfCorp.TeamService.Repositories.Team.PostgreSQL
{
    public class TeamRepository : ITeamRepository
    {
        readonly private TeamDbContext dbContext;

        public TeamRepository(TeamDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Models.Team> List()
        {
            return this.dbContext.Teams.Include(t => t.Members).ToArray();
        }

        public Models.Team Get(Guid id)
        {
            return this.dbContext.Teams.FirstOrDefault(t => t.ID == id);
        }

        public Models.Team Update(Models.Team team)
        {
            this.dbContext.Entry(team).State = EntityState.Modified;
            this.dbContext.SaveChanges();
            return team;            
        }

        public Models.Team Add(Models.Team team)
        {
            this.dbContext.Teams.Add(team);
            this.dbContext.SaveChanges();
            return team;
        }

        public Models.Team Delete(Guid id)
        {
            Models.Team team = this.Get(id);
            this.dbContext.Remove(team);
            this.dbContext.SaveChanges();
            return team;
        }
    }
}