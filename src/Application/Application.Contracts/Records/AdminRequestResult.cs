namespace Application.Contracts.Records;

public record AdminRequestResult
{
    private AdminRequestResult()
    {
    }

    public sealed record Success : AdminRequestResult;

    public sealed record Fail : AdminRequestResult;
}