// See https://aka.ms/new-console-template for more information

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Rabbitmq.Application.Producers.Context.Book;
using Rabbitmq.Shared;
using Rabbitmq.Producers;
using System.Reflection;





IServiceCollection services = new ServiceCollection();

 services.AddTransient(c => new ProducerBook());
 services.AddTransient(c => new ProducerLogs());
services.AddMediatR(c =>
{
    c.RegisterServicesFromAssemblies(Assembly.GetAssembly(typeof(BookCommand))!);

});



//build services
var serviceProvider = services.BuildServiceProvider();
var mediator = serviceProvider.GetService<IMediator>();

//Publisch new created book model

var commands = new List<BookCommand>
{
    new BookCommand(new BookModel { Id = "14", Author = "Asty Noukeu", Title = "Take it easy..." },
        nameof(ModelState.Created)),
    new BookCommand(
        new BookModel { Id = "587", Author = "Ngounou Noukeu", Title = "Journey in the jungle 1" },
        nameof(ModelState.Created)),
    new BookCommand(
        new BookModel { Id = "588", Author = "Ngounou Noukeu", Title = "Journey in the jungle 2" },
        nameof(ModelState.Created)),
    new BookCommand(
        new BookModel { Id = "589", Author = "Ngounou Noukeu", Title = "Journey in the jungle 3" },
        nameof(ModelState.Deleted)),
    new BookCommand(
        new BookModel { Id = "599", Author = "Ngounou Noukeu", Title = "Journey in the jungle 4" },
        nameof(ModelState.Deleted)),
    new BookCommand(
        new BookModel { Id = "544", Author = "Ngounou Noukeu", Title = "Journey in the jungle 7" },
        nameof(ModelState.Blocked)),
    new BookCommand(
        new BookModel { Id = "545", Author = "Ngounou Noukeu", Title = "Journey in the jungle 8" },
        nameof(ModelState.Blocked))
};
for (int i = 0; i < 3; i++)
{
    Task.Delay(1500).GetAwaiter().GetResult();
    foreach (var bookCommand in commands)
    {
        Task.Delay(500).GetAwaiter().GetResult();
        mediator?.Send(bookCommand).GetAwaiter();
    }
}




//Use work queue

//new TaskQueueCommand("Hello World again...").Send();

//Use exchange pattern

//for (int i = 0; i < 50; i++)
//{
//    mediator?.Send(new LogCommand($"[{i}] logging..."));
//    Thread.Sleep(500);
//}

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
