using Application.Contracts.Account;
using Application.Contracts.Records;

namespace Application.Account;

public class AccountServiceProxy : IAccountService
{
    private readonly IAccountService _accountService;

    public AccountServiceProxy(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public LoginResult Login(int accountId, string password)
    {
        return password.Length is < 6 or > 6 ? new LoginResult.Fail() : _accountService.Login(accountId, password);
    }

    public CreateResult CreateAccount(int accountId, string password)
    {
        return password.Length is < 6 or > 6
            ? new CreateResult.Fail()
            : _accountService.CreateAccount(accountId, password);
    }

    public void Logout()
    {
        _accountService.Logout();
    }

    public async Task Update()
    {
        await _accountService.Update();
    }
}