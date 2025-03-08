using Application.Models.Account;

namespace Application.Abstractions.Repositories;

public interface IAccountRepository
{
    Task<AccountData?> GetAccount(int accountId, string password);

    Task UpdateAccountInformation(int accountId, decimal balance);

    Task CreateAccount(int accountId, string password);

    IAsyncEnumerable<AdminAccountData> GetAllAccounts();

    Task<bool> Contains(int accountId);
}