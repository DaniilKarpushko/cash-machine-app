namespace Application.Models.Operations;

public record OperationData(int AccountId, OperationType OperationType, decimal Amount, DateTime Time);