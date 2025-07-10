using Rabbitmq.Shared;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Rabbitmq.Producers;

public class ProducerBook : BaseProducer
{

    protected override void Init()
    {
        Channel = ChannelFactory.CreateChannel();

        Channel.QueueDeclare(queue: ExchangeContext.BookDirect,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        Channel.ExchangeDeclare(exchange: ExchangeContext.BookDirect, type: ExchangeType.Direct);       
    }

    public override void Publish(object obj, string severity = "")
    {
        var message = JsonSerializer.Serialize(obj);
        var body = Encoding.UTF8.GetBytes(message);
        Channel.BasicPublish(exchange: ExchangeContext.BookDirect,
                                routingKey: severity,
                                basicProperties: null,
                                body: body);
        Console.WriteLine(" [x] Sent {0}", message);
        Console.WriteLine("Waiting for sending the next message...");
    }
}
