using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Rabbitmq.Consumers;

public abstract class BaseConsumer
{
    protected readonly IModel? Channel;
    protected BaseConsumer(IModel channel)
    {
        ArgumentNullException.ThrowIfNull(channel);
        Channel = channel;
        Init();
    }

    protected virtual void Init()
    {
    }

    protected void HandleMessage(object? model, BasicDeliverEventArgs ea)
    {
        ReceivedMessage?.Invoke(model, ea);
    }

    public event EventHandler<BasicDeliverEventArgs>? ReceivedMessage;

}
