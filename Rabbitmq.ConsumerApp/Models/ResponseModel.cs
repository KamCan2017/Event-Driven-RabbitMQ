namespace Rabbitmq.ConsumerApp.Models;

public class ResponseModel<T> where T : class
{
    public T Content { get; set; } = null!;   
    public bool Success { get; set; }
    
    public string Error { get; set; } = string.Empty;   
}