using System.Globalization;
using Application.Contracts.Account;
using Application.Contracts.Logger;
using Application.Models.Operations;
using Presentation.Console.Scenarios.Interfaces;
using Spectre.Console;

namespace Presentation.Console.Scenarios.CashWorkScenarios.ShowHistoryScenario;

public class HistoryScenario : IScenario
{
    private readonly ILoggerService _loggerService;
    private readonly ICurrentAccountService _accountService;

    public HistoryScenario(ILoggerService loggerService, ICurrentAccountService accountService)
    {
        _loggerService = loggerService;
        _accountService = accountService;
        Name = "Show history";
    }

    public string Name { get; }

    public async Task Run()
    {
        var table = new Table();
        table.AddColumn("AccountID")
            .AddColumn("Operation")
            .AddColumn("Amount")
            .AddColumn("Date");
        if (_accountService.CurrentAccount is not null)
        {
            await foreach (OperationData data in _loggerService.GetLogsByAccountId(
                               _accountService.CurrentAccount.AccountId))
            {
                string[] row =
                {
                    data.AccountId.ToString(new NumberFormatInfo()),
                    data.OperationType.ToString(),
                    data.Amount.ToString(new NumberFormatInfo()),
                    data.Time.ToString(new DateTimeFormatInfo()).Substring(0, 10),
                };

                table.AddRow(row);
            }
        }

        AnsiConsole.Write(table);
        System.Console.ReadKey(true);
    }
}