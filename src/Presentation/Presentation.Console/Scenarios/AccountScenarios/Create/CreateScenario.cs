using Application.Contracts.Account;
using Presentation.Console.Scenarios.Interfaces;
using Spectre.Console;

namespace Presentation.Console.Scenarios.AccountScenarios.Create;

public class CreateScenario : IScenario
{
    private readonly IAccountService _service;

    public CreateScenario(IAccountService service)
    {
        _service = service;
        Name = "Create account";
    }

    public string Name { get; }

    public Task Run()
    {
        int accountId = AnsiConsole.Ask<int>("[green]Create accountId: [/]");
        string password = AnsiConsole.Ask<string>("[green]Create password(6 symbols): [/]");

        AnsiConsole.WriteLine(_service.CreateAccount(accountId, password).ToString());
        System.Console.ReadKey(true);

        return Task.CompletedTask;
    }
}