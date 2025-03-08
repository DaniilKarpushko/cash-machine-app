using Application.Account.CurrentAccountService;
using Application.CashService;
using Application.Contracts.Account;
using Application.Contracts.Cash;
using Application.Contracts.Records;
using Application.Models.Account;

namespace RepositoryTests;

public class RepositoriesMoqTests
{
    private readonly ICurrentAccountService _currentAccount;
    private readonly ICashService _cashService;

    public RepositoriesMoqTests()
    {
        _currentAccount = new CurrentAccountManager();
        _currentAccount.CurrentAccount = new AccountData(1, 10);
        _cashService = new CashService(_currentAccount);
    }

    [Fact]
    public void AddMoneyTestCaseTrue()
    {
        _cashService.AddMoney(100);
        Assert.Equal(110, _currentAccount.CurrentAccount?.Balance);
    }

    [Fact]
    public void EnoughMoneyToTakeTestCaseTrue()
    {
        RequestResult result = _cashService.TakeMoney(9);
        Assert.NotNull(_currentAccount.CurrentAccount);
        var expected = new RequestResult.Success(
            _currentAccount.CurrentAccount.AccountId,
            _currentAccount.CurrentAccount.Balance);
        Assert.Equal(expected, result);
        Assert.Equal(1, _currentAccount.CurrentAccount?.Balance);
    }

    [Fact]
    public void NotEnoughMoneyToTakeTestCaseError()
    {
        RequestResult result = _cashService.TakeMoney(100);
        Assert.NotNull(_currentAccount.CurrentAccount);
        Assert.NotNull(_currentAccount.CurrentAccount);
        var expected = new RequestResult.NotEnoughMoneyError(_currentAccount.CurrentAccount.Balance);
        Assert.Equal(expected, result);
    }
}