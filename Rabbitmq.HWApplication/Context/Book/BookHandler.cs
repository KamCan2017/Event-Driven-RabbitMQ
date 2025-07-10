using MediatR;
using Rabbitmq.Producers;

namespace Rabbitmq.Application.Producers.Context.Book;

public class BookHandler(ProducerBook producer) : IRequestHandler<BookCommand, Unit>
{   
    public Task<Unit> Handle(BookCommand request, CancellationToken cancellationToken)
    {
        producer.Publish(request.Model, request.Severity);
        return Task.FromResult(Unit.Value);
    }
}
