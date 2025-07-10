
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Rabbitmq.ConsumerApp.Models;
using Rabbitmq.Consumers;

namespace Rabbitmq.ConsumerApp;

public class ConsumerCollector: IConsumerCollector
{
    public ConsumerCollector(IModel channel)
    {
        ArgumentNullException.ThrowIfNull(channel);
        new ConsumerBookCreated(channel).ReceivedMessage += async (_, args) => { await HandleData(args, InfoState.New); };
        new ConsumerBookDeleted(channel).ReceivedMessage += async (_, args) => { await HandleData(args, InfoState.Deleted); };
        new ConsumerBookBlocked(channel).ReceivedMessage += async (_, args) => { await HandleData(args, InfoState.Blocked); };
    }
    
    public EventHandler<InfoStateModel>? OnBookRetrieved { get; set; }
    
    private async Task HandleData(BasicDeliverEventArgs args, InfoState state)
    {
        var response = await BookHandler.Handle(args);
        var info = new InfoStateModel { State = response.Success ? state : InfoState.Failed, Message = response.Success ? response.Content.ToString() : response.Error};
        OnBookRetrieved?.Invoke(null, info);
    }
}


public interface IConsumerCollector
{
 EventHandler<InfoStateModel>? OnBookRetrieved { get; set; }
}