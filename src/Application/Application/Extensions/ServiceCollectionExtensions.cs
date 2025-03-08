using Application.Access;
using Application.Account;
using Application.Account.CurrentAccountService;
using Application.Admin;
using Application.Admin.CurrentAdminService;
using Application.CashService;
using Application.Contracts.AccessService;
using Application.Contracts.Account;
using Application.Contracts.Admin;
using Application.Contracts.Cash;
using Application.Contracts.Logger;
using Application.Logger;
using Lab5.Application.Contracts.Admin;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IAccountService, AccountService>();
        collection.AddScoped<AccountServiceProxy>();
        collection.AddScoped<ICurrentAccountService, CurrentAccountManager>();
        collection.AddScoped<IFullAccessService, FullAccessService>();
        collection.AddScoped<IAdminService, AdminService>();
        collection.AddScoped<ICurrentAdminService, CurrentAdminService>();
        collection.AddScoped<ICashService, global::Application.CashService.CashService>();
        collection.AddScoped<ILoggerService, OperationLogger>();
        collection.AddScoped<CashServiceWithLogger>();

        return collection;
    }
}