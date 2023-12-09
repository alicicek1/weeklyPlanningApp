using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using toDoaPP.Command;
using toDoApp.Command.Client;
using toDoApp.Command.Models.ProjectModels;
using toDoApp.Core.Entities;
using toDoApp.Database;
using static System.Console;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

var config = configuration.GetSection("Config").Get<Config>();

if (config == null || config.PlanningApiClientSetting?.RequestingEndpoints.Length == 0)
{
    WriteLine("An error occured while reading config file.");
    return;
}


var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddScoped<IHttpClient, CustomHttpClient>();
        services.AddSingleton<IDatabase, Database>();
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var httpClient = serviceProvider.GetRequiredService<IHttpClient>();
    var database = serviceProvider.GetRequiredService<IDatabase>();


    if (config.PlanningApiClientSetting != null && config.PlanningApiClientSetting.RequestingEndpoints.Any())
    {
        foreach (var url in config.PlanningApiClientSetting.RequestingEndpoints)
        {
            httpClient.GetAsync<ProjectModel>(url)
                .ContinueWith(responseTask =>
                {
                    var response = responseTask.Result;
                    if (response == null || response.Tasks.Count <= 0) return;
                    foreach (var task in response.Tasks)
                    {
                        var id = database.AddTask(new TaskEntity
                        {
                            Name = task.Name,
                            Duration = task.Duration,
                            Difficulty = task.Difficulty,
                            AssignedDeveloper = task.AssignedDeveloper
                        });
                        WriteLine($"TaskEntity added. Id:{id}");
                    }
                });
        }
    }
}