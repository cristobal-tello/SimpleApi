using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatlerWaldorfCorp.TeamService.LocationClient;
using StatlerWaldorfCorp.TeamService.Repositories;
using StatlerWaldorfCorp.TeamService.Repositories.Location.PostgreSQL;
using StatlerWaldorfCorp.TeamService.Repositories.Team.PostgreSQL;

namespace StatlerWaldorfCorp.TeamService
{
    public class Startup
    {
        public static string[] Args { get; set; } = new string[] { };
        public static IConfigurationRoot Configuration { get; set;  }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .AddCommandLine(Startup.Args);

            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            //services.AddScoped<ILocationRecordRepository, InMemoryLocationRecordRepository>();
            var locationUrl = Configuration.GetSection("location:url").Value;
            services.AddSingleton<ILocationClient>(new HttpLocationClient(locationUrl));

            var teamServiceConnectionString = Configuration.GetSection("teamservice:cstr").Value;
            var locationServiceConnectionString = Configuration.GetSection("locationservice:cstr").Value;

            services.AddEntityFrameworkNpgsql().AddDbContext<TeamDbContext>(options => options.UseNpgsql(teamServiceConnectionString));
            services.AddEntityFrameworkNpgsql().AddDbContext<LocationDbContext>(options => options.UseNpgsql(locationServiceConnectionString));
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
