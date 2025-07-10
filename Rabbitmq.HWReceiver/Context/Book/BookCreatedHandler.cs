using MediatR;
using Rabbitmq.Shared;
using System.Text;
using System.Text.Json;

namespace Rabbitmq.Application.Consumers.Context.Book;

public class BookCreatedHandler: IRequestHandler<BookCreatedCommand, Unit>
{

    public Task<Unit> Handle(BookCreatedCommand request, CancellationToken cancellationToken)
    {
        var body = request.Args.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var book = JsonSerializer.Deserialize<BookModel>(message);

        if (book == null)
        {
            Console.WriteLine($"Receive data is not type of {nameof(BookModel)}.");
            return Task.FromResult(Unit.Value);
        }

        Console.WriteLine($" [x] Created: {message}");
        return Task.FromResult(Unit.Value);

    }    
}
