using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StatlerWaldorfCorp.EventProcessor.Events;
using System.Text;

namespace StatlerWaldorfCorp.EventProcessor.Queues.AMQP
{
    public class AmqpEventSubscriber : IEventSubscriber
    {
        private readonly ILogger logger;
        private readonly EventingBasicConsumer consumer;
        private readonly QueueOptions queueOptions;
        private readonly IModel channel;
        private string consumerTag;

        public AmqpEventSubscriber(ILogger<AmqpEventSubscriber> logger, IOptions<QueueOptions> queueOptions,  EventingBasicConsumer consumer)
        {
            this.logger = logger;
            this.queueOptions = queueOptions.Value;
            this.consumer = consumer;

            this.channel = consumer.Model;

            Initialize();
        }

        private void Initialize()
        {
            channel.QueueDeclare(
                queue: queueOptions.MemberLocationRecordedEventQueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
            logger.LogInformation($"Initialized event subscriber for queue {queueOptions.MemberLocationRecordedEventQueueName}");

            consumer.Received += (ch, ea) =>
            {
                var body = ea.Body;
                var msg = Encoding.UTF8.GetString(body);
                var evt = JsonConvert.DeserializeObject<MemberLocationRecordedEvent>(msg);
                logger.LogInformation($"Received incoming event, {body.Length} bytes.");
                if (MemberLocationRecordedEventReceived != null)
                {
                    MemberLocationRecordedEventReceived(evt);
                }
                channel.BasicAck(ea.DeliveryTag, false);
            };
        }

        public event MemberLocationRecordedEventReceivedDelegate MemberLocationRecordedEventReceived;

        public void Subscribe()
        {
            consumerTag = channel.BasicConsume(queueOptions.MemberLocationRecordedEventQueueName, false, consumer);
            logger.LogInformation("Subscribed to queue.");
        }

        public void Unsubscribe()
        {
            channel.BasicCancel(consumerTag);
            logger.LogInformation("Unsubscribed from queue.");
        }
    }
}