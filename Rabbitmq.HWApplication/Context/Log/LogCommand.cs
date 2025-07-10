using MediatR;

namespace Rabbitmq.Application.Producers.Context.Log;

public record LogCommand(string Message): IRequest<Unit>
{
}
