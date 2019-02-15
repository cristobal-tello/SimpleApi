using Microsoft.EntityFrameworkCore;
using Models = StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.Repositories.Team.PostgreSQL
{
    public class TeamDbContext : DbContext
    {
        public TeamDbContext(DbContextOptions<TeamDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("uuid-ossp");
        }

        public DbSet<Models.Team> Teams { get; set; }
        
    }
}
