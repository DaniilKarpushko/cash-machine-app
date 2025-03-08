using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Account;
using Application.Contracts.Cash;
using Presentation.Console.Scenarios.Interfaces;

namespace Presentation.Console.Scenarios.CashWorkScenarios.TakeScenario;

public class TakeMoneyScenarioProvider : IScenarioProvider
{
    private readonly ICurrentAccountService _currentAccount;
    private readonly IAccountService _accountService;
    private readonly ICashService _cashService;

    public TakeMoneyScenarioProvider(ICurrentAccountService currentAccount, ICashService cashService, IAccountService accountService)
    {
        _currentAccount = currentAccount;
        _accountService = accountService;
        _cashService = cashService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccount.CurrentAccount is not null)
        {
            scenario = new TakeMoneyScenario(_cashService, _accountService);
            return true;
        }

        scenario = null;
        return false;
    }
}