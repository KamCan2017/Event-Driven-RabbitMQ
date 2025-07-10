using System.Diagnostics;
using Rabbitmq.Shared;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Rabbitmq.Consumers;

public class ConsumerLogs(IModel channel): BaseConsumer(channel)
{    
 
    protected override void Init()
    {
        Channel.ExchangeDeclare(exchange: ExchangeContext.Logs, type: ExchangeType.Fanout);

        // declare a server-named queue
        var queueName = Channel.QueueDeclare().QueueName;
        Channel.QueueBind(queue: queueName,
                          exchange: ExchangeContext.Logs,
                          routingKey: string.Empty);

        Debug.WriteLine(" [*] Waiting for logs...");

        var consumer = new EventingBasicConsumer(Channel);
        consumer.Received += HandleMessage;
        Channel.BasicConsume(queue: queueName,
                             autoAck: true,
                             consumer: consumer);

    }


}
