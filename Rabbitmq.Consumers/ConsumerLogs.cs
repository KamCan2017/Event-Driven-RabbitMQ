using Rabbitmq.Shared;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Rabbitmq.Consumers;

public class ConsumerLogs: BaseConsumer
{    
 
    protected override void Init()
    {
        var channel = ChannelFactory.CreateChannel();
        channel.ExchangeDeclare(exchange: ExchangeContext.Logs, type: ExchangeType.Fanout);

        // declare a server-named queue
        var queueName = channel.QueueDeclare().QueueName;
        channel.QueueBind(queue: queueName,
                          exchange: ExchangeContext.Logs,
                          routingKey: string.Empty);

        Console.WriteLine(" [*] Waiting for logs...");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += HandleMessage;
        channel.BasicConsume(queue: queueName,
                             autoAck: true,
                             consumer: consumer);

    }


}
