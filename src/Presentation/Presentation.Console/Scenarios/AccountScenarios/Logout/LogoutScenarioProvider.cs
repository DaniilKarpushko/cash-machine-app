using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Account;
using Presentation.Console.Scenarios.Interfaces;

namespace Presentation.Console.Scenarios.AccountScenarios.Logout;

public class LogoutScenarioProvider : IScenarioProvider
{
    private readonly ICurrentAccountService _currentAccount;
    private readonly IAccountService _service;

    public LogoutScenarioProvider(ICurrentAccountService currentAccount, IAccountService service)
    {
        _currentAccount = currentAccount;
        _service = service;
    }

    public bool TryGetScenario([NotNullWhen(true)]out IScenario? scenario)
    {
        if (_currentAccount.CurrentAccount is not null)
        {
            scenario = new LogoutScenario(_service);
            return true;
        }

        scenario = null;
        return false;
    }
}