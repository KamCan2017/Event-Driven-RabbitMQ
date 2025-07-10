using RabbitMQ.Client;

namespace Rabbitmq.Shared;

public static class ChannelFactory
{
    public static IModel CreateChannel()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        var connection = factory.CreateConnection();
        return connection.CreateModel();
    }
}
