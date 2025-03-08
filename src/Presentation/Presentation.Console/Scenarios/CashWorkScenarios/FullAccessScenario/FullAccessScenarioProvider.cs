using System.Diagnostics.CodeAnalysis;
using Application.Contracts.AccessService;
using Application.Contracts.Admin;
using Application.Models;
using Presentation.Console.Scenarios.Interfaces;

namespace Presentation.Console.Scenarios.CashWorkScenarios.FullAccessScenario;

public class FullAccessScenarioProvider : IScenarioProvider
{
    private readonly IFullAccessService _fullAccessService;
    private readonly ICurrentAdminService _currentAdmin;

    public FullAccessScenarioProvider(IFullAccessService fullAccessService, ICurrentAdminService currentAdmin)
    {
        _fullAccessService = fullAccessService;
        _currentAdmin = currentAdmin;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAdmin.State is AdminState.Connected)
        {
            scenario = new AllAccountsScenario(_fullAccessService);
            return true;
        }

        scenario = null;
        return false;
    }
}