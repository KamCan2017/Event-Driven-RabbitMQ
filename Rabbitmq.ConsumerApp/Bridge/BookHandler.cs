using System.Text;
using System.Text.Json;
using RabbitMQ.Client.Events;
using Rabbitmq.ConsumerApp.Models;
using Rabbitmq.Shared;

namespace Rabbitmq.ConsumerApp;

public static class BookHandler
{
    private static int _failedCounter;
    public static async Task<ResponseModel<BookModel>> Handle(BasicDeliverEventArgs args)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(args);
            _failedCounter++;
            //Simulate failed messages
            if(_failedCounter % 7 == 0) throw new Exception("Simulate some failed messages.");
            var body = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var book = await Task.FromResult(JsonSerializer.Deserialize<BookModel>(message));

            return book == null
                ? new ResponseModel<BookModel>
                    { Success = false, Error = $"Receive data is not type of {nameof(BookModel)}." }
                : new ResponseModel<BookModel> { Success = true, Content = book};
        }
        catch (Exception exception)
        {
             return new ResponseModel<BookModel> { Success = false, Error = exception.Message };           
        }
    }
}