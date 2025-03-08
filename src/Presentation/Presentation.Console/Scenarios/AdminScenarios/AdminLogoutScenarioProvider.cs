using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Admin;
using Application.Models;
using Lab5.Application.Contracts.Admin;
using Lab5.Presentation.Console.Scenarios.AdminScenarios;
using Presentation.Console.Scenarios.Interfaces;

namespace Presentation.Console.Scenarios.AdminScenarios;

public class AdminLogoutScenarioProvider : IScenarioProvider
{
    private IAdminService _service;
    private ICurrentAdminService _currentAdmin;

    public AdminLogoutScenarioProvider(IAdminService service, ICurrentAdminService currentAdmin)
    {
        _service = service;
        _currentAdmin = currentAdmin;
    }

    public bool TryGetScenario([NotNullWhen(true)]out IScenario? scenario)
    {
        if (_currentAdmin.State == AdminState.Connected)
        {
            scenario = new AdminLogoutScenario(_service);
            return true;
        }

        scenario = null;
        return false;
    }
}