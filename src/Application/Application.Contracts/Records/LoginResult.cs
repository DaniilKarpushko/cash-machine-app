namespace Application.Contracts.Records;

public abstract record LoginResult
{
    private LoginResult()
    {
    }

    public sealed record Fail : LoginResult;

    public sealed record Success : LoginResult;
}