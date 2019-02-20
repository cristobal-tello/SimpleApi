using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace StatlerWaldorfCorp.EventProcessor.Queues.AMQP
{
    public class AmqpEventingConsumer : EventingBasicConsumer
    {
        public AmqpEventingConsumer(ILogger<AmqpEventingConsumer> logger, IConnectionFactory factory) : base(factory.CreateConnection().CreateModel())
        {
        }
    }
}