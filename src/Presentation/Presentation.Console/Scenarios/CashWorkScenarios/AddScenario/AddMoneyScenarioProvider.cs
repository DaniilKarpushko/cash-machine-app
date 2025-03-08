using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Account;
using Application.Contracts.Cash;
using Presentation.Console.Scenarios.Interfaces;

namespace Presentation.Console.Scenarios.CashWorkScenarios.AddScenario;

public class AddMoneyScenarioProvider : IScenarioProvider
{
    private readonly ICurrentAccountService _currentAccount;
    private readonly ICashService _cashService;
    private readonly IAccountService _accountService;

    public AddMoneyScenarioProvider(ICurrentAccountService currentAccount, ICashService cashService, IAccountService accountService)
    {
        _currentAccount = currentAccount;
        _cashService = cashService;
        _accountService = accountService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccount.CurrentAccount is not null)
        {
            scenario = new AddMoneyScenario(_cashService, _accountService);
            return true;
        }

        scenario = null;
        return false;
    }
}