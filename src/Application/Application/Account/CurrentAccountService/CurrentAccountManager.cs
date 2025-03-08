using Application.Contracts.Account;
using Application.Models.Account;

namespace Application.Account.CurrentAccountService;

public class CurrentAccountManager : ICurrentAccountService
{
    public AccountData? CurrentAccount { get; set; }
}