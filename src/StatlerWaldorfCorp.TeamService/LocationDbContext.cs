using Microsoft.EntityFrameworkCore;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.LocationService.TeamService
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

        public DbSet<LocationRecord> LocationRecords { get; set; }
        
    }
}
