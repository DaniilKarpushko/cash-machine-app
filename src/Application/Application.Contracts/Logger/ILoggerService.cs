using Application.Models.Operations;

namespace Application.Contracts.Logger;

public interface ILoggerService
{
    void LogOperation(OperationData operationData);
    IAsyncEnumerable<OperationData> GetLogsByAccountId(int accountId);
}