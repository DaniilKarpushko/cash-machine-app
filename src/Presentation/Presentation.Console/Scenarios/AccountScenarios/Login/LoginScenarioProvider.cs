using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Account;
using Presentation.Console.Scenarios.Interfaces;

namespace Presentation.Console.Scenarios.AccountScenarios.Login;

public class LoginScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _service;
    private readonly ICurrentAccountService _currentAccount;

    public LoginScenarioProvider(IAccountService service, ICurrentAccountService currentAccount)
    {
        _service = service;
        _currentAccount = currentAccount;
    }

    public bool TryGetScenario([NotNullWhen(true)]out IScenario? scenario)
    {
        if (_currentAccount.CurrentAccount is null)
        {
            scenario = new LoginScenario(_service);
            return true;
        }

        scenario = null;
        return false;
    }
}