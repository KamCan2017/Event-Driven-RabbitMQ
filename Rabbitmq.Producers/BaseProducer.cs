using Rabbitmq.Producers.Interfaces;
using RabbitMQ.Client;

namespace Rabbitmq.Producers;

public abstract class BaseProducer: IProducer
{
    protected readonly IModel? Channel;
    protected BaseProducer(IModel channel)
    {
        ArgumentNullException.ThrowIfNull(channel);
        Channel = channel;
        Init();
    }

    protected virtual void Init()
    { }

    public virtual void Publish(string message)
    {

    }

    public virtual void Publish(object obj, string severity = "")
    {

    }
}
