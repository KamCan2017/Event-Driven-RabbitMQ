using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Rabbitmq.Application.Consumers.Context.Book;
using Rabbitmq.Consumers;
using System.Reflection;


IServiceCollection services = new ServiceCollection();

services.AddMediatR(c =>
{
    c.RegisterServicesFromAssemblies(Assembly.GetAssembly(typeof(BookCreatedCommand))!);

});



//build services
var serviceProvider = services.BuildServiceProvider();
var mediator = serviceProvider.GetService<IMediator>();


//ConsumerTaskQueue consumerTaskQueue = new ConsumerTaskQueue();
//consumerTaskQueue.ReceivedMessage += (model, args) => { mediator?.Send(new TaskQueueCommand(model, args)); };

//ConsumerLogs consumerLogs = new ();
//consumerLogs.ReceivedMessage += (model, args) => { mediator?.Send(new LogCommand(model, args)); };

ConsumerBookCreated consumerBookCreated = new ();
consumerBookCreated.ReceivedMessage += (model, args) => { mediator?.Send(new BookCreatedCommand(model, args)); };

ConsumerBookDeleted consumerBookDeleted = new();
consumerBookDeleted.ReceivedMessage += (model, args) => { mediator?.Send(new BookDeletedCommand(model, args)); };

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

