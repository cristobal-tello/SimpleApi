using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StatlerWaldorfCorp.EventProcessor.Events;
using StatlerWaldorfCorp.EventProcessor.Models.Location;
using StatlerWaldorfCorp.EventProcessor.Queues;
using StatlerWaldorfCorp.EventProcessor.Queues.AMQP;

namespace StatlerWaldorfCorp.EventProcessor
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddOptions();

            services.Configure<QueueOptions>(Configuration.GetSection("QueueOptions"));
            services.Configure<AmqpOptions>(Configuration.GetSection("amqp"));

          //  services.AddRedisConnectionMultiplexer(Configuration);

            services.AddTransient(typeof(IConnectionFactory), typeof(AmqpConnectionFactory));
            services.AddTransient(typeof(EventingBasicConsumer), typeof(AmqpEventingConsumer));

            services.AddSingleton(typeof(ILocationCache), typeof(LocationCache));

            services.AddSingleton(typeof(IEventSubscriber), typeof(AmqpEventSubscriber));
            services.AddSingleton(typeof(IEventEmitter), typeof(AmqpEventEmitter));
            services.AddSingleton(typeof(IEventProcessor), typeof(MemberLocationEventProcessor));
        }

        // Singletons are lazy instantiation.. so if we don't ask for an instance during startup,
        // they'll never get used.
        public void Configure(IApplicationBuilder app,
                IHostingEnvironment env,
                ILoggerFactory loggerFactory,
                IEventProcessor eventProcessor)
        {
            app.UseMvc();

            eventProcessor.Start();
        }
    }
}