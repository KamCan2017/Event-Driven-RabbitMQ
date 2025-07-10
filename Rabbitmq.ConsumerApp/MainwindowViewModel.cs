using System.Collections.ObjectModel;
using Rabbitmq.ConsumerApp.Models;

namespace Rabbitmq.ConsumerApp;

public class MainWindowViewModel:BaseNotifyPropertyModel
{
    private ObservableCollection<InfoStateModel> _infoStates = [];
    private readonly Lock _lock = new ();

    public MainWindowViewModel(IConsumerCollector consumerCollector)
    {
        ArgumentNullException.ThrowIfNull(consumerCollector);
        consumerCollector.OnBookRetrieved += (sender, model) => OnConsumerBookRetrieved(model);       
    }

    public ObservableCollection<InfoStateModel> InfoStates 
    {
        get => _infoStates;
        set => SetField(ref _infoStates, value);       
    }

    public string Title { get; set; } = "Notifications";
    
    public string NotificationCount { get; set; } = string.Empty;   
    
    private void OnConsumerBookRetrieved(InfoStateModel arg)
    {
        lock (_lock)
        {
            var source = InfoStates.ToList();
            source.Add(arg);
            InfoStates = new ObservableCollection<InfoStateModel>(source);
            NotificationCount = InfoStates.Count > 0 ? InfoStates.Count.ToString() : string.Empty;  
            OnPropertyChanged(nameof(NotificationCount));
        }
    }
}