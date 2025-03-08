using System.Globalization;
using Application.Contracts.AccessService;
using Application.Models.Account;
using Presentation.Console.Scenarios.Interfaces;
using Spectre.Console;

namespace Presentation.Console.Scenarios.CashWorkScenarios.FullAccessScenario;

public class AllAccountsScenario : IScenario
{
    private IFullAccessService _service;

    public AllAccountsScenario(IFullAccessService service)
    {
        _service = service;
        Name = "Show all accounts";
    }

    public string Name { get; }

    public async Task Run()
    {
        var table = new Table();
        table.AddColumn("AccountID")
            .AddColumn("Password")
            .AddColumn("Balance");

        await foreach (AdminAccountData data in _service.GetAllAccounts())
        {
            string[] row =
            {
                data.AccountId.ToString(new NumberFormatInfo()),
                data.Password,
                data.Balance.ToString(new NumberFormatInfo()),
            };

            table.AddRow(row);
        }

        AnsiConsole.Write(table);
        System.Console.ReadKey(true);
    }
}