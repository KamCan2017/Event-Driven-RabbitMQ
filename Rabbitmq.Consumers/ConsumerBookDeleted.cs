using Rabbitmq.Shared;

namespace Rabbitmq.Consumers;

public class ConsumerBookDeleted: ConsumerBook
{
    protected override string Severity => nameof(ModelState.Deleted);
}

public class ConsumerBookBlocked: ConsumerBook
{
    protected override string Severity => nameof(ModelState.Blocked);
}
