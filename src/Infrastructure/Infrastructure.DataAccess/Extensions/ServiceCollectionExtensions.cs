using Application.Abstractions.Configs;
using Application.Abstractions.Repositories;
using Infrastructure.DataAccess.Configs;
using Infrastructure.DataAccess.Plugins;
using Infrastructure.DataAccess.Repositories;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Itmo.Dev.Platform.Postgres.Plugins;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccess(
        this IServiceCollection collection,
        Action<PostgresConnectionConfiguration> configuration,
        string adminConfiguration)
    {
        collection.AddPlatformPostgres(builder => builder.Configure(configuration));
        collection.AddPlatformMigrations(typeof(ServiceCollectionExtensions).Assembly);

        collection.AddSingleton<IDataSourcePlugin, MappingPlugin>();

        collection.AddScoped<IAccountRepository, AccountRepository>();
        collection.AddScoped<IOperationsRepository, OperationsRepository>();
        collection.AddSingleton<IAdminPasswordRepository, AdminPasswordRepository>(x =>
            new AdminPasswordRepository(adminConfiguration));

        return collection;
    }
}