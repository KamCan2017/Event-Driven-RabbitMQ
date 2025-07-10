using System.Diagnostics;
using Rabbitmq.Shared;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Rabbitmq.Consumers
{
    public abstract class ConsumerBook(IModel channel): BaseConsumer(channel)
    {
        protected virtual string Severity => nameof(ModelState.Created);

        protected override void Init()
        {
            Channel.ExchangeDeclare(exchange: ExchangeContext.BookDirect, type: ExchangeType.Direct);

            // declare a server-named queue
            var queueName = Channel.QueueDeclare().QueueName;
            Channel.QueueBind(queue: queueName,
                              exchange: ExchangeContext.BookDirect,
                              routingKey: Severity);

            Debug.WriteLine(" [*] Waiting for books...");

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += HandleMessage;
            Channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

        }
    }
}
