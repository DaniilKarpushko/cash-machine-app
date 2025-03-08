using Application.Models.Account;

namespace Application.Contracts.Account;

public interface ICurrentAccountService
{
    AccountData? CurrentAccount { get; set; }
}