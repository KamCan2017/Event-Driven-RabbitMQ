namespace Rabbitmq.ConsumerApp.Models;

public class InfoStateModel: BaseNotifyPropertyModel
{
    private string? _message;
    private InfoState _state;

    public string? Message
    {
        get => _message;
        set => SetField(ref _message, value);       
    }
    public InfoState State 
    {
        get => _state;
        set => SetField(ref _state, value);       
    }
}

public enum InfoState
{
    Failed,
    New,
    Deleted,
    Blocked
}