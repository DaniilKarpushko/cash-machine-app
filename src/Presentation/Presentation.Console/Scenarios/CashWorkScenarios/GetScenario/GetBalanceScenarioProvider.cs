using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Account;
using Application.Contracts.Cash;
using Presentation.Console.Scenarios.Interfaces;

namespace Presentation.Console.Scenarios.CashWorkScenarios.GetScenario;

public class GetBalanceScenarioProvider : IScenarioProvider
{
    private readonly ICurrentAccountService _currentAccount;
    private readonly ICashService _cashService;

    public GetBalanceScenarioProvider(ICurrentAccountService currentAccount, ICashService cashService)
    {
        _currentAccount = currentAccount;
        _cashService = cashService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccount.CurrentAccount is not null)
        {
            scenario = new GetBalanceScenario(_cashService);
            return true;
        }

        scenario = null;
        return false;
    }
}