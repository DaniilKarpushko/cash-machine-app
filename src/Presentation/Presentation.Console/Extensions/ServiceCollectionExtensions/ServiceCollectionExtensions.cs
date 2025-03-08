using Application.Account;
using Application.CashService;
using Application.Contracts.Account;
using Lab5.Presentation.Console.Scenarios;
using Lab5.Presentation.Console.Scenarios.AdminScenarios;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Console.Scenarios;
using Presentation.Console.Scenarios.AccountScenarios.Create;
using Presentation.Console.Scenarios.AccountScenarios.Login;
using Presentation.Console.Scenarios.AccountScenarios.Logout;
using Presentation.Console.Scenarios.AdminScenarios;
using Presentation.Console.Scenarios.CashWorkScenarios.AddScenario;
using Presentation.Console.Scenarios.CashWorkScenarios.FullAccessScenario;
using Presentation.Console.Scenarios.CashWorkScenarios.GetScenario;
using Presentation.Console.Scenarios.CashWorkScenarios.ShowHistoryScenario;
using Presentation.Console.Scenarios.CashWorkScenarios.TakeScenario;
using Presentation.Console.Scenarios.Interfaces;

namespace Presentation.Console.Extensions.ServiceCollectionExtensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();
        collection.AddScoped<IScenarioProvider, LoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LogoutScenarioProvider>();
        collection.AddScoped<IScenarioProvider, CreateScenarioProvider>(x => new CreateScenarioProvider(
            x.GetRequiredService<AccountServiceProxy>(),
            x.GetRequiredService<ICurrentAccountService>()));

        collection.AddScoped<IScenarioProvider, AdminLoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AdminLogoutScenarioProvider>();

        collection.AddScoped<IScenarioProvider, GetBalanceScenarioProvider>(x =>
            new GetBalanceScenarioProvider(
                x.GetRequiredService<ICurrentAccountService>(),
                x.GetRequiredService<CashServiceWithLogger>()));
        collection.AddScoped<IScenarioProvider, AddMoneyScenarioProvider>(x => new AddMoneyScenarioProvider(
            x.GetRequiredService<ICurrentAccountService>(),
            x.GetRequiredService<CashServiceWithLogger>(),
            x.GetRequiredService<IAccountService>()));
        collection.AddScoped<IScenarioProvider, TakeMoneyScenarioProvider>(x => new TakeMoneyScenarioProvider(
            x.GetRequiredService<ICurrentAccountService>(),
            x.GetRequiredService<CashServiceWithLogger>(),
            x.GetRequiredService<IAccountService>()));

        collection.AddScoped<IScenarioProvider, HistoryScenarioProvider>();

        collection.AddScoped<IScenarioProvider, FullAccessScenarioProvider>();

        return collection;
    }
}