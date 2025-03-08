using Application.Contracts.Account;
using Application.Contracts.Cash;
using Application.Contracts.Records;
using Presentation.Console.Scenarios.Interfaces;
using Spectre.Console;

namespace Presentation.Console.Scenarios.CashWorkScenarios.AddScenario;

public class AddMoneyScenario : IScenario
{
    private readonly ICashService _cashService;
    private readonly IAccountService _accountService;

    public AddMoneyScenario(ICashService cashService, IAccountService accountService)
    {
        _cashService = cashService;
        _accountService = accountService;
        Name = "Add money";
    }

    public string Name { get; }

    public Task Run()
    {
        int amount = AnsiConsole.Ask<int>("[green]How much: [/]");
        RequestResult res = _cashService.AddMoney(amount);
        _accountService.Update();
        AnsiConsole.WriteLine(res.ToString());
        System.Console.ReadKey(true);

        return Task.CompletedTask;
    }
}