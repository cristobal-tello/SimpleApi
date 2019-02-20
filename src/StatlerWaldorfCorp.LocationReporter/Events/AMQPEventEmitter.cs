using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using StatlerWaldorfCorp.LocationReporter.Models;
using System;
using System.Text;

namespace StatlerWaldorfCorp.LocationReporter.Events
{

    public class AmqpEventEmitter : IEventEmitter
    {
        public const string QUEUE_LOCATIONRECORDED = "memberlocationrecorded";

        private readonly AmqpOptions rabbitOptions;
        private readonly ConnectionFactory connectionFactory;

        public AmqpEventEmitter(IOptions<AmqpOptions> amqpOptions)
        {
            this.rabbitOptions = amqpOptions.Value;

            connectionFactory = new ConnectionFactory();

            connectionFactory.UserName = rabbitOptions.Username;
            connectionFactory.Password = rabbitOptions.Password;
            connectionFactory.VirtualHost = rabbitOptions.VirtualHost;
            connectionFactory.HostName = rabbitOptions.HostName;
            connectionFactory.Uri = new Uri(rabbitOptions.Uri);
        }

        public void EmitLocationRecordedEvent(MemberLocationRecordedEvent locationRecordedEvent)
        {
            using (IConnection conn = connectionFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: QUEUE_LOCATIONRECORDED,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );
                    string jsonPayload = locationRecordedEvent.toJson();
                    var body = Encoding.UTF8.GetBytes(jsonPayload);
                    channel.BasicPublish(
                        exchange: string.Empty,
                        routingKey: QUEUE_LOCATIONRECORDED,
                        basicProperties: null,
                        body: body
                    );
                }
            }
        }
    }
}