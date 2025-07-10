using MediatR;
using RabbitMQ.Client.Events;

namespace Rabbitmq.Application.Consumers.Context.Logs;

public record LogCommand(object? Model, BasicDeliverEventArgs Args): IRequest<Unit>;
