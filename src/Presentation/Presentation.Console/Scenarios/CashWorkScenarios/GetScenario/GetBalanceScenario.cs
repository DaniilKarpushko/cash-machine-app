using Application.Contracts.Cash;
using Application.Contracts.Records;
using Presentation.Console.Scenarios.Interfaces;
using Spectre.Console;

namespace Presentation.Console.Scenarios.CashWorkScenarios.GetScenario;

public class GetBalanceScenario : IScenario
{
    private readonly ICashService _cashService;

    public GetBalanceScenario(ICashService cashService)
    {
        _cashService = cashService;
        Name = "Show balance";
    }

    public string Name { get; }

    public Task Run()
    {
        RequestResult result = _cashService.GetCurrentBalance();
        if (result is RequestResult.Success success)
            AnsiConsole.WriteLine($"Your balance is {success.Balance}");
        else
            AnsiConsole.WriteLine(result.ToString());

        System.Console.ReadKey(true);
        
        return Task.CompletedTask;
    }
}