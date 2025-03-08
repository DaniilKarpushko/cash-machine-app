namespace Application.Contracts.Records;

public abstract record CreateResult
{
    private CreateResult()
    {
    }

    public sealed record Success : CreateResult;

    public sealed record Fail() : CreateResult;
}