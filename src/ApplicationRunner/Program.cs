using Application.Extensions;
using Infrastructure.DataAccess.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Console.Extensions.ServiceCollectionExtensions;
using Presentation.Console.Scenarios;
using Spectre.Console;

var collection = new ServiceCollection();

collection.AddApplication().AddInfrastructureDataAccess(
    configuration =>
    {
        configuration.Host = "localhost";
        configuration.Port = 6432;
        configuration.Username = "postgres";
        configuration.Password = "postgres";
        configuration.Database = "postgres";
        configuration.SslMode = "Prefer";
    },
    "D:\\c#\\Labs\\Lab5\\DaniilKarpushko\\AdminConfigurator.txt").AddPresentationConsole();

using ServiceProvider serviceProvider = collection.BuildServiceProvider();
using IServiceScope scope = serviceProvider.CreateScope();
scope.UseInfrastructureDataAccess();
ScenarioRunner scenarioRunner = scope.ServiceProvider.GetRequiredService<ScenarioRunner>();

while (true)
{
    await scenarioRunner.Run();
    AnsiConsole.Clear();
}