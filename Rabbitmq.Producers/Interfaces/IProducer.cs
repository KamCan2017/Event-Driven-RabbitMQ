namespace Rabbitmq.Producers.Interfaces;

public interface IProducer
{
    void Publish(string message);
    void Publish(object obj, string severity = "");
}
