using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Account;
using Application.Contracts.Logger;
using Presentation.Console.Scenarios.Interfaces;

namespace Presentation.Console.Scenarios.CashWorkScenarios.ShowHistoryScenario;

public class HistoryScenarioProvider : IScenarioProvider
{
    private readonly ICurrentAccountService _currentAccount;
    private readonly ILoggerService _loggerService;

    public HistoryScenarioProvider(ICurrentAccountService currentAccount, ILoggerService loggerService)
    {
        _currentAccount = currentAccount;
        _loggerService = loggerService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccount.CurrentAccount is not null)
        {
            scenario = new HistoryScenario(_loggerService, _currentAccount);
            return true;
        }

        scenario = null;
        return false;
    }
}