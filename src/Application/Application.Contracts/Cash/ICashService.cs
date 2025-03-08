using Application.Contracts.Records;

namespace Application.Contracts.Cash;

public interface ICashService
{
    RequestResult AddMoney(decimal amount);
    RequestResult TakeMoney(decimal amount);
    RequestResult GetCurrentBalance();
}