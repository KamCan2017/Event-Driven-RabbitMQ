using Rabbitmq.Shared;

namespace Rabbitmq.Consumers;

public class ConsumerBookCreated: ConsumerBook
{
    protected override string Severity => nameof(ModelState.Created);
}
