using Lab5.Application.Contracts.Admin;
using Presentation.Console.Scenarios.Interfaces;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.AdminScenarios;

public class AdminLogoutScenario : IScenario
{
    private IAdminService _adminService;

    public AdminLogoutScenario(IAdminService adminService)
    {
        _adminService = adminService;
        Name = "Logout from Admin console";
    }

    public string Name { get; }

    public Task Run()
    {
        if (AnsiConsole.Confirm("[green]Are you sure?[/]"))
            _adminService.Logout();

        return Task.CompletedTask;
    }
}