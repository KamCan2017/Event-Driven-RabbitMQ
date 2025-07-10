using MediatR;
using Rabbitmq.Application.Consumers.Context.Logs;
using Rabbitmq.Shared;
using System.Text;
using System.Text.Json;

namespace Rabbitmq.Application.Consumers.Context.Book;

public class BookDeletedHandler: IRequestHandler<BookDeletedCommand, Unit>
{

    public Task<Unit> Handle(BookDeletedCommand request, CancellationToken cancellationToken)
    {
        var body = request.Args.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var book = JsonSerializer.Deserialize<BookModel>(message);

        if (book == null)
        {
            Console.WriteLine($"Receive data is not type of {nameof(BookModel)}.");
            return Task.FromResult(Unit.Value);
        }

        Console.WriteLine($" [x] Deleted: {message}");
        return Task.FromResult(Unit.Value);

    }    
}
