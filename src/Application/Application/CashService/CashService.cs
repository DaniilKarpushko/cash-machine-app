using Application.Contracts.Account;
using Application.Contracts.Cash;
using Application.Contracts.Records;

namespace Application.CashService;

public class CashService : ICashService
{
    private readonly ICurrentAccountService _accountService;

    public CashService(ICurrentAccountService accountService)
    {
        _accountService = accountService;
    }

    public RequestResult AddMoney(decimal amount)
    {
        if (_accountService.CurrentAccount?.AccountId == null)
            return new RequestResult.ConnectionAccountFailure();

        _accountService.CurrentAccount = _accountService.CurrentAccount with
        {
            Balance = _accountService.CurrentAccount.Balance + amount
        };

        return new RequestResult.Success(
            _accountService.CurrentAccount.AccountId,
            _accountService.CurrentAccount.Balance);
    }

    public RequestResult TakeMoney(decimal amount)
    {
        if (_accountService.CurrentAccount?.AccountId == null)
            return new RequestResult.ConnectionAccountFailure();

        if (_accountService.CurrentAccount.Balance < amount)
            return new RequestResult.NotEnoughMoneyError(_accountService.CurrentAccount.Balance);

        _accountService.CurrentAccount = _accountService.CurrentAccount with
        {
            Balance = _accountService.CurrentAccount.Balance - amount
        };

        return new RequestResult.Success(
            _accountService.CurrentAccount.AccountId,
            _accountService.CurrentAccount.Balance);
    }

    public RequestResult GetCurrentBalance()
    {
        if (_accountService.CurrentAccount?.AccountId == null)
            return new RequestResult.ConnectionAccountFailure();

        return new RequestResult.Success(
            _accountService.CurrentAccount.AccountId,
            _accountService.CurrentAccount.Balance);
    }
}