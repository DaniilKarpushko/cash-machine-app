using Application.Contracts.Records;
using Lab5.Application.Contracts.Admin;
using Presentation.Console.Scenarios.Interfaces;
using Spectre.Console;

namespace Presentation.Console.Scenarios.AdminScenarios;

public class AdminLoginScenario : IScenario
{
    private IAdminService _service;

    public AdminLoginScenario(IAdminService currentAdmin)
    {
        _service = currentAdmin;
        Name = "Login as Admin";
    }

    public string Name { get; }

    public Task Run()
    {
        string password = AnsiConsole.Ask<string>("[green]Please enter the password: [/]");
        LoginResult result = _service.Login(password);
        AnsiConsole.WriteLine(result.ToString());
        System.Console.ReadKey(true);

        return Task.CompletedTask;
    }
}