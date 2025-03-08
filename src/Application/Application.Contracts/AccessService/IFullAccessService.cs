using Application.Models.Account;

namespace Application.Contracts.AccessService;

public interface IFullAccessService
{
    public IAsyncEnumerable<AdminAccountData> GetAllAccounts();
}