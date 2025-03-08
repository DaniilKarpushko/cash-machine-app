using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Account;
using Presentation.Console.Scenarios.Interfaces;

namespace Presentation.Console.Scenarios.AccountScenarios.Create;

public class CreateScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _accountService;
    private readonly ICurrentAccountService _currentAccount;

    public CreateScenarioProvider(IAccountService accountService, ICurrentAccountService currentAccount)
    {
        _accountService = accountService;
        _currentAccount = currentAccount;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccount.CurrentAccount is null)
        {
            scenario = new CreateScenario(_accountService);
            return true;
        }

        scenario = null;
        return false;
    }
}