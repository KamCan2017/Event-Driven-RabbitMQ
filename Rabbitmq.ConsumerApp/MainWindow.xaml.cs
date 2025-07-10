
using System.Windows;
using Rabbitmq.Shared;

namespace Rabbitmq.ConsumerApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel(new ConsumerCollector(ChannelFactory.CreateChannel()));
    }
}