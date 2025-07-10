using MediatR;
using System.Text;

namespace Rabbitmq.Application.Consumers.Context.Logs;

public class LogHandler : IRequestHandler<LogCommand, Unit>
{
    public Task<Unit> Handle(LogCommand request, CancellationToken cancellationToken)
    {
        var body = request.Args.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($" [x] {message}");
        Thread.Sleep(500);
        return Task.FromResult(Unit.Value); 
    }
}
