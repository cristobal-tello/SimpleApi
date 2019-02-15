using Microsoft.EntityFrameworkCore;
using Models = StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.Repositories.Location.PostgreSQL
{
    public class LocationDbContext : DbContext
    {
        public LocationDbContext(DbContextOptions<LocationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("uuid-ossp");
        }

        public DbSet<Models.Location> Locations { get; set; }
        
    }
}
