using Application.Contracts.Account;
using Application.Contracts.Cash;
using Presentation.Console.Scenarios.Interfaces;
using Spectre.Console;

namespace Presentation.Console.Scenarios.CashWorkScenarios.TakeScenario;

public class TakeMoneyScenario : IScenario
{
    private readonly IAccountService _accountService;
    private readonly ICashService _cashService;

    public TakeMoneyScenario(ICashService cashService, IAccountService accountService)
    {
        Name = "Withdraw money";
        _cashService = cashService;
        _accountService = accountService;
    }

    public string Name { get; }

    public Task Run()
    {
        int amount = AnsiConsole.Ask<int>("[green]How much: [/]");
        AnsiConsole.WriteLine(_cashService.TakeMoney(amount).ToString());
        _accountService.Update();
        System.Console.ReadKey(true);

        return Task.CompletedTask;
    }
}