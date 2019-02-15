using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StatlerWaldorfCorp.TeamService.Repositories.Team.PostgreSQL
{
    public class TeamDbContextFactory : IDesignTimeDbContextFactory<TeamDbContext>
    {
        public TeamDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TeamDbContext>();
            var connectionString = Startup.Configuration.GetSection("teamservice:cstr").Value;
            optionsBuilder.UseNpgsql(connectionString);

            return new TeamDbContext(optionsBuilder.Options);
        }
    }
}