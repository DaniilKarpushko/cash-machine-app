using Application.Contracts.Cash;
using Application.Contracts.Logger;
using Application.Contracts.Records;
using Application.Models.Operations;

namespace Application.CashService;

public class CashServiceWithLogger : ICashService
{
    private readonly ICashService _cashService;
    private readonly ILoggerService _logger;

    public CashServiceWithLogger(ICashService cashService, ILoggerService logger)
    {
        _cashService = cashService;
        _logger = logger;
    }

    public RequestResult AddMoney(decimal amount)
    {
        RequestResult res = _cashService.AddMoney(amount);
        if (res is RequestResult.Success success)
        {
            _logger.LogOperation(new OperationData(
                success.AccountId,
                OperationType.Replenishment,
                amount,
                DateTime.Now));
        }

        return res;
    }

    public RequestResult TakeMoney(decimal amount)
    {
        RequestResult res = _cashService.TakeMoney(amount);
        if (res is RequestResult.Success success)
        {
            _logger.LogOperation(new OperationData(
                success.AccountId,
                OperationType.Withdraw,
                amount,
                DateTime.Now));
        }

        return res;
    }

    public RequestResult GetCurrentBalance()
    {
        return _cashService.GetCurrentBalance();
    }
}