using Rabbitmq.Shared;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Rabbitmq.Consumers
{
    public abstract class ConsumerBook: BaseConsumer
    {
        protected virtual string Severity => nameof(ModelState.Created);

        protected override void Init()
        {
            var channel = ChannelFactory.CreateChannel();
            channel.ExchangeDeclare(exchange: ExchangeContext.BookDirect, type: ExchangeType.Direct);

            // declare a server-named queue
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName,
                              exchange: ExchangeContext.BookDirect,
                              routingKey: Severity);

            Console.WriteLine(" [*] Waiting for books...");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += HandleMessage;
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

        }
    }
}
