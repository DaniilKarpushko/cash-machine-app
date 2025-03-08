using Application.Abstractions.Repositories;
using Application.Models.Operations;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace Infrastructure.DataAccess.Repositories;

public class OperationsRepository : IOperationsRepository
{
    private readonly IPostgresConnectionProvider _postgresConnectionProvider;

    public OperationsRepository(IPostgresConnectionProvider provider)
    {
        _postgresConnectionProvider = provider;
    }

    public async IAsyncEnumerable<OperationData> GetOperations(int accountId)
    {
        const string getRequest = """
                                  SELECT * FROM operations
                                  WHERE account_id = @accountId
                                  """;
        NpgsqlConnection connection = await _postgresConnectionProvider.GetConnectionAsync(default);

        await using var command = new NpgsqlCommand(getRequest, connection);
        command
            .AddParameter("accountId", accountId);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            yield return new OperationData(
                reader.GetInt32(1),
                await reader.GetFieldValueAsync<OperationType>(2),
                reader.GetDecimal(4),
                reader.GetDateTime(3));
        }
    }

    public async Task InsertOperation(OperationData operationData)
    {
        const string addRequest = """
                                  INSERT INTO operations(account_id, operation, amount, date)
                                  VALUES (@accountId, @operation, @amount, @time);
                                  """;
        NpgsqlConnection connection = await _postgresConnectionProvider.GetConnectionAsync(default);

        await using var command = new NpgsqlCommand(addRequest, connection);
        command
            .AddParameter("accountId", operationData.AccountId)
            .AddParameter("operation", operationData.OperationType)
            .AddParameter("amount", operationData.Amount)
            .AddParameter("time", operationData.Time);
        await command.ExecuteNonQueryAsync();
    }
}