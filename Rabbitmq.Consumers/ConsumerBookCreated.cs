using RabbitMQ.Client;
using Rabbitmq.Shared;

namespace Rabbitmq.Consumers;

public class ConsumerBookCreated(IModel channel): ConsumerBook(channel)
{
    protected override string Severity => nameof(ModelState.Created);
}
