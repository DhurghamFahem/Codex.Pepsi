namespace Codex.Pepsi.Abstractions.Accounts.DTOs.Queries;

public interface IGetAccountsQuery
{
    Guid[]? Ids { get; set; }
    Guid? BusinessId { get; set; }
    string[]? ExternalIds { get; set; }
    DateTimeOffset? CreatedAtGTE { get; set; }
    DateTimeOffset? CreatedAtLTE { get; set; }
    string? Query { get; set; }
    Guid? ParentId { get; set; }
    string? Number { get; set; }
    int Offset { get; set; }
    int Limit { get; set; }
}
