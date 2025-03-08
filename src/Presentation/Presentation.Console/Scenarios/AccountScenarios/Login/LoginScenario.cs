using Application.Contracts.Account;
using Application.Contracts.Records;
using Presentation.Console.Scenarios.Interfaces;
using Spectre.Console;

namespace Presentation.Console.Scenarios.AccountScenarios.Login;

public class LoginScenario : IScenario
{
    private readonly IAccountService _accountService;

    public LoginScenario(IAccountService accountService)
    {
        _accountService = accountService;
        Name = "Login";
    }

    public string Name { get; }

    public Task Run()
    {
        int accountId = AnsiConsole.Ask<int>("[green]Write your accountId: [/]");
        string password = AnsiConsole.Ask<string>("[green]Write your password: [/]");

        LoginResult loginResult = _accountService.Login(accountId, password);
        AnsiConsole.WriteLine(loginResult.ToString());
        System.Console.ReadKey(true);

        return Task.CompletedTask;
    }
}