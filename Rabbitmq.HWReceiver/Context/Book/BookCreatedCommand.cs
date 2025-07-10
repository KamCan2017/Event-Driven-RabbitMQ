using MediatR;
using RabbitMQ.Client.Events;

namespace Rabbitmq.Application.Consumers.Context.Book;

public record BookCreatedCommand(object? Model, BasicDeliverEventArgs Args): IRequest<Unit>;
