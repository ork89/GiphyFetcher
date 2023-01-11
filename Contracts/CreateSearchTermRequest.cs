namespace Contracts
{
    public record CreateSearchTermRequest
    (
        string Term,
        int Limit,
        int Offset,
        string Rating,
        string Language
    );
}