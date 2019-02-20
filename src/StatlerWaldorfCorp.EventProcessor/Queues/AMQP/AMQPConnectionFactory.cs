using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Linq;
using Microsoft.Extensions.Logging;
using StatlerWaldorfCorp.EventProcessor.Models;
using System;

namespace StatlerWaldorfCorp.EventProcessor.Queues.AMQP
{
    public class AmqpConnectionFactory : ConnectionFactory
    {
        protected AmqpOptions amqpOptions;

        public AmqpConnectionFactory(
            ILogger<AmqpConnectionFactory> logger,
            IOptions<AmqpOptions> serviceOptions) : base()
        {
            this.amqpOptions = serviceOptions.Value;

            this.UserName = amqpOptions.Username;
            this.Password = amqpOptions.Password;
            this.VirtualHost = amqpOptions.VirtualHost;
            this.HostName = amqpOptions.HostName;
            this.Uri = new Uri(amqpOptions.Uri);

            logger.LogInformation($"AMQP Connection configured for URI : {amqpOptions.Uri}");
        }
    }
}