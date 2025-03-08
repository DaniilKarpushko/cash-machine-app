namespace Application.Contracts.Records;

public abstract record RequestResult
{
    private RequestResult()
    {
    }

    public sealed record NotEnoughMoneyError(
        decimal CurrentBalance,
        string ErrorMessage = "Not enough money on account!") : RequestResult;

    public sealed record InvalidSumError(string ErrorMessage = "Please, put valid sum!") : RequestResult;

    public sealed record ConnectionAccountFailure(string ErrorMessage = "User is not connected!") : RequestResult;

    public sealed record Success(int AccountId, decimal Balance) : RequestResult;
}