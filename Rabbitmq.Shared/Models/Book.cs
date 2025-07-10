namespace Rabbitmq.Shared;

public class BookModel
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    
    public override string ToString() => $"{Title} written by {Author}";   
}
