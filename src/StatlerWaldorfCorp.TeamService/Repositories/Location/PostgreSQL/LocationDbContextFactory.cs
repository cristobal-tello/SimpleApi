using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StatlerWaldorfCorp.TeamService.Repositories.Location.PostgreSQL
{
    public class LocationDbContextFactory : IDesignTimeDbContextFactory<LocationDbContext>
    {
        public LocationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LocationDbContext>();
            var connectionString = Startup.Configuration.GetSection("locationservice:cstr").Value;
            optionsBuilder.UseNpgsql(connectionString);

            return new LocationDbContext(optionsBuilder.Options);
        }
    }
}