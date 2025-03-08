using Application.Models.Operations;

namespace Application.Abstractions.Repositories;

public interface IOperationsRepository
{
    IAsyncEnumerable<OperationData> GetOperations(int accountId);

    Task InsertOperation(OperationData operationData);
}