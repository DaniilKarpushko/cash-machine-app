using Application.Abstractions.Repositories;
using Application.Contracts.AccessService;
using Application.Models.Account;

namespace Application.Access;

public class FullAccessService : IFullAccessService
{
    private IAccountRepository _accountRepository;

    public FullAccessService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async IAsyncEnumerable<AdminAccountData> GetAllAccounts()
    {
        await foreach (var account in _accountRepository.GetAllAccounts())
            yield return account;
    }
}