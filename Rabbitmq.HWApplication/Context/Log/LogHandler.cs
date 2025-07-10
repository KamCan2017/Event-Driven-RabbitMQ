using MediatR;
using Rabbitmq.Producers;

namespace Rabbitmq.Application.Producers.Context.Log;

public class TaskQueueHandler(ProducerLogs producer): IRequestHandler<LogCommand, Unit>
{
    public Task<Unit> Handle(LogCommand request, CancellationToken cancellationToken)
    {
        producer.Publish(request.Message);
        return Task.FromResult(Unit.Value);
    }
}
