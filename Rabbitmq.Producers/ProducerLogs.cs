using System.Diagnostics;
using Rabbitmq.Shared;
using RabbitMQ.Client;
using System.Text;

namespace Rabbitmq.Producers;

public class ProducerLogs (IModel channel): BaseProducer(channel)
{

    protected override void Init()
    {
        Channel?.QueueDeclare(queue: ExchangeContext.Logs,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        Channel.ExchangeDeclare(exchange: ExchangeContext.Logs, type: ExchangeType.Fanout);       
    }

    public override void Publish(string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        message = $"[{DateTime.Now} - {message}";
        var body = Encoding.UTF8.GetBytes(message);
        Channel.BasicPublish(exchange: ExchangeContext.Logs,
                                routingKey: "",
                                basicProperties: null,
                                body: body);
        Debug.WriteLine(" [x] Sent {0}", message);
        Debug.WriteLine("Waiting for sending the next message...");
    }
}
