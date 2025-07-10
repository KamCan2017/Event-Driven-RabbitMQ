using System.Windows;
using System.Windows.Controls;
using Rabbitmq.ConsumerApp.Models;

namespace Rabbitmq.ConsumerApp.Template;

public class ItemTemplateSelector: DataTemplateSelector
{
    public DataTemplate? DeleteBookTemplate { get; set; }
    public DataTemplate? FailedBookTemplate { get; set; }
    public DataTemplate? BlockedBookTemplate { get; set; }
    public DataTemplate? DefaultMessageTemplate { get; set; }
    
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is not InfoStateModel infoState) return DefaultMessageTemplate!;
        return infoState.State switch
        {
            InfoState.Failed  => FailedBookTemplate!,
            InfoState.New => DefaultMessageTemplate!,
            InfoState.Deleted => DeleteBookTemplate!,
            InfoState.Blocked => BlockedBookTemplate!,
            _ => throw new ArgumentOutOfRangeException($"Datatemplate for state {infoState.State} not found")
        };
    }
}