using Application.Contracts.Account;
using Presentation.Console.Scenarios.Interfaces;
using Spectre.Console;

namespace Presentation.Console.Scenarios.AccountScenarios.Logout;

public class LogoutScenario : IScenario
{
    private readonly IAccountService _accountService;

    public LogoutScenario(IAccountService accountService)
    {
        _accountService = accountService;
        Name = "Logout";
    }

    public string Name { get; }

    public Task Run()
    {
        bool ans = AnsiConsole.Confirm("[green]Are you sure?[/]");
        if (ans)
            _accountService.Logout();

        return Task.CompletedTask;
    }
}