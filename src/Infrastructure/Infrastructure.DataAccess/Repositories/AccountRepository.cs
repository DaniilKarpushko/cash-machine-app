using Application.Abstractions.Repositories;
using Application.Models.Account;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.UnitOfWork;
using Npgsql;

namespace Infrastructure.DataAccess.Repositories;

public class AccountRepository : IAccountRepository
{
    private IPostgresConnectionProvider _postgresConnectionProvider;
    private IUnitOfWork _unitOfWork;

    public AccountRepository(IPostgresConnectionProvider postgresConnectionProvider, IUnitOfWork unitOfWork)
    {
        _postgresConnectionProvider = postgresConnectionProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<AccountData?> GetAccount(int accountId, string password)
    {
        const string request = """
                               SELECT * FROM Accounts
                               WHERE account_id = @accountId AND
                                     password = @password;
                               """;
        NpgsqlConnection connection =
            await _postgresConnectionProvider.GetConnectionAsync(default).ConfigureAwait(true);

        await using var command = new NpgsqlCommand(request, connection);

        await using NpgsqlDataReader reader = await command
            .AddParameter("accountId", accountId)
            .AddParameter("password", password)
            .ExecuteReaderAsync();

        if (await reader.ReadAsync() is false)
            return null;

        return new AccountData(
            AccountId: reader.GetInt32(0),
            Balance: reader.GetDecimal(2));
    }

    public async Task UpdateAccountInformation(int accountId, decimal balance)
    {
        const string request = """
                               UPDATE accounts
                               SET balance = :balance
                               WHERE account_id = :accountId
                               """;
        NpgsqlConnection connection = await _postgresConnectionProvider.GetConnectionAsync(default);

        await using var command = new NpgsqlCommand(request, connection);
        command.AddParameter("balance", balance);
        command.AddParameter("accountId", accountId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task CreateAccount(int accountId, string password)
    {
        const decimal balance = 0;
        const string createRequest = """
                                     INSERT INTO accounts(account_id, password, balance)
                                     VALUES (:accountId, :password, :balance)
                                     """;
        NpgsqlConnection connection = await _postgresConnectionProvider.GetConnectionAsync(default);

        await using var command = new NpgsqlCommand(createRequest, connection);
        command
            .AddParameter("accountId", accountId)
            .AddParameter("password", password)
            .AddParameter("balance", balance);

        await command.ExecuteNonQueryAsync();
    }

    public async IAsyncEnumerable<AdminAccountData> GetAllAccounts()
    {
        const string request = """
                               SELECT * FROM accounts
                               """;

        NpgsqlConnection connection = await _postgresConnectionProvider.GetConnectionAsync(default);

        await using var command = new NpgsqlCommand(request, connection);
        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            yield return new AdminAccountData(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetDecimal(2));
        }
    }

    public async Task<bool> Contains(int accountId)
    {
        const string request = """
                               SELECT * FROM Accounts
                               WHERE account_id = @accountId
                               """;
        NpgsqlConnection connection = await _postgresConnectionProvider.GetConnectionAsync(default);
        await using var command = new NpgsqlCommand(request, connection);

        await using NpgsqlDataReader reader = await command
            .AddParameter("accountId", accountId)
            .ExecuteReaderAsync();

        return await reader.ReadAsync();
    }
}