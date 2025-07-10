using System.Diagnostics;
using Rabbitmq.Shared;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Rabbitmq.Producers;

public class ProducerBook (IModel channel) : BaseProducer(channel)
{

    protected override void Init()
    {
        Channel?.QueueDeclare(queue: ExchangeContext.BookDirect,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        Channel.ExchangeDeclare(exchange: ExchangeContext.BookDirect, type: ExchangeType.Direct);       
    }

    public override void Publish(object obj, string severity = "")
    {
        ArgumentNullException.ThrowIfNull(obj);
        var message = JsonSerializer.Serialize(obj);
        var body = Encoding.UTF8.GetBytes(message);
        Channel.BasicPublish(exchange: ExchangeContext.BookDirect,
                                routingKey: severity,
                                basicProperties: null,
                                body: body);
        Debug.WriteLine(" [x] Sent {0}", message);
        Debug.WriteLine("Waiting for sending the next message...");
    }
}
