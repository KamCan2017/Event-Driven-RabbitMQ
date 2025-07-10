using Rabbitmq.Shared;
using RabbitMQ.Client;
using System.Text;

namespace Rabbitmq.Producers;

public class ProducerLogs : BaseProducer
{

    protected override void Init()
    {
        Channel = ChannelFactory.CreateChannel();

        Channel.QueueDeclare(queue: ExchangeContext.Logs,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        Channel.ExchangeDeclare(exchange: ExchangeContext.Logs, type: ExchangeType.Fanout);       
    }

    public override void Publish(string message)
    {
        message = $"[{DateTime.Now} - {message}";
        var body = Encoding.UTF8.GetBytes(message);
        Channel.BasicPublish(exchange: ExchangeContext.Logs,
                                routingKey: "",
                                basicProperties: null,
                                body: body);
        Console.WriteLine(" [x] Sent {0}", message);
        Console.WriteLine("Waiting for sending the next message...");
    }
}
