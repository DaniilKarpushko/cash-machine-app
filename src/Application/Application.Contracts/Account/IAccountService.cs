using Application.Contracts.Records;

namespace Application.Contracts.Account;

public interface IAccountService
{
    LoginResult Login(int accountId, string password);

    CreateResult CreateAccount(int accountId, string password);

    void Logout();

    Task Update();
}