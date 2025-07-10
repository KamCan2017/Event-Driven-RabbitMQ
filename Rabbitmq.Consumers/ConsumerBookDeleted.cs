using RabbitMQ.Client;
using Rabbitmq.Shared;

namespace Rabbitmq.Consumers;

public class ConsumerBookDeleted(IModel channel): ConsumerBook(channel)
{
    protected override string Severity => nameof(ModelState.Deleted);
}

public class ConsumerBookBlocked (IModel channel): ConsumerBook(channel)
{
    protected override string Severity => nameof(ModelState.Blocked);
}
