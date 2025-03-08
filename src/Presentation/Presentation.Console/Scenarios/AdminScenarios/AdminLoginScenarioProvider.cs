using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Account;
using Application.Contracts.Admin;
using Application.Models;
using Lab5.Application.Contracts.Admin;
using Presentation.Console.Scenarios.Interfaces;

namespace Presentation.Console.Scenarios.AdminScenarios;

public class AdminLoginScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _service;
    private readonly ICurrentAdminService _currentAdmin;
    private ICurrentAccountService _currentAccount;

    public AdminLoginScenarioProvider(IAdminService service, ICurrentAdminService currentAdmin, ICurrentAccountService currentAccount)
    {
        _service = service;
        _currentAdmin = currentAdmin;
        _currentAccount = currentAccount;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAdmin.State == AdminState.Disconnected && _currentAccount.CurrentAccount is null)
        {
            scenario = new AdminLoginScenario(_service);
            return true;
        }

        scenario = null;
        return false;
    }
}