using Application.Abstractions.Repositories;
using Application.Contracts.Logger;
using Application.Models.Operations;

namespace Application.Logger;

public class OperationLogger : ILoggerService
{
    private readonly IOperationsRepository _repository;

    public OperationLogger(IOperationsRepository repository)
    {
        _repository = repository;
    }

    public void LogOperation(OperationData operationData)
    {
        _repository.InsertOperation(operationData).GetAwaiter().GetResult();
    }

    public async IAsyncEnumerable<OperationData> GetLogsByAccountId(int accountId)
    {
        await foreach (var operation in _repository.GetOperations(accountId))
            yield return operation;
    }
}