using Application.Abstractions.Repositories;
using Application.Contracts.Account;
using Application.Contracts.Records;
using Application.Models.Account;

namespace Application.Account;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICurrentAccountService _currentAccountManager;

    public AccountService(IAccountRepository accountRepository, ICurrentAccountService currentAccountManager)
    {
        _accountRepository = accountRepository;
        _currentAccountManager = currentAccountManager;
    }

    public LoginResult Login(int accountId, string password)
    {
        AccountData? res = _accountRepository.GetAccount(accountId, password).Result;
        if (res is null)
        {
            return new LoginResult.Fail();
        }

        _currentAccountManager.CurrentAccount = res;
        return new LoginResult.Success();
    }

    public void Logout()
    {
        _currentAccountManager.CurrentAccount = null;
    }

    public CreateResult CreateAccount(int accountId, string password)
    {
        if (_accountRepository.Contains(accountId).Result)
        {
            return new CreateResult.Fail();
        }

        _accountRepository.CreateAccount(accountId, password).GetAwaiter().GetResult();
        _currentAccountManager.CurrentAccount = new AccountData(accountId, 0);
        return new CreateResult.Success();
    }

    public Task Update()
    {
        if (_currentAccountManager.CurrentAccount is not null)
        {
            return _accountRepository.UpdateAccountInformation(
                _currentAccountManager.CurrentAccount.AccountId,
                _currentAccountManager.CurrentAccount.Balance);
        }

        return Task.CompletedTask;
    }
}