using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using taskMeneger;

// var serviceProvider = new ServiceCollection()
//     .AddDbContext<TaskMenegerDbContext>(options =>
//     {
//         options.UseNpgsql("Host=localhost;Port=5432;Database=your_database;Username=your_user;Password=your_password;");
//     })
//     .AddSingleton<ITaskRespository, JsonClassRepository>()
//     .AddSingleton<ITaskService, TaskService1.TaskService>()
//     .AddSingleton<TaskControler.TaskController>();
//     //.BuildServiceProvider();
//
//     var host = Host.CreateDefaultBuilder();
//
//     var serviceProvider2 = serviceProvider.BuildServiceProvider();
// var taskController = serviceProvider2.GetRequiredService<TaskControler.TaskController>();
// taskController.Choosing();
var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<TaskMenegerDbContext>(o =>
{
    o.UseNpgsql("Host=localhost;Port=5432;Database=your_database;Username=your_user;Password=your_password;");
});

builder.Services.AddScoped<ITaskRespository, PostGresTaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService1.TaskService>();
builder.Services.AddSingleton<TaskControler.TaskController>();

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<TaskMenegerDbContext>();
    await dbContext.Database.MigrateAsync();
}

//await host.RunAsync();

using (var scope = host.Services.CreateScope())
{
    var taskController = scope.ServiceProvider.GetRequiredService<TaskControler.TaskController>();
    taskController.Choosing();
}