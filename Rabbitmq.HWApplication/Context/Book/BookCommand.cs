using MediatR;
using Rabbitmq.Shared;

namespace Rabbitmq.Application.Producers.Context.Book;

public record BookCommand(BookModel Model, string Severity): IRequest<Unit>
{
}
