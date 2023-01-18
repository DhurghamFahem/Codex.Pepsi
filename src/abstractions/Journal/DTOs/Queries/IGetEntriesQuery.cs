namespace Codex.Pepsi.Abstractions.Journal.DTOs.Queries;

public interface IGetEntriesQuery
{
    Guid[]? Ids { get; set; }
    string[]? ExternalIds { get; set; }
    Guid[]? AccountIds { get; set; }
    Guid[]? TransactionIds { get; set; }
    DateTimeOffset? TimestampGTE { get; set; }
    DateTimeOffset? TimestampLTE { get; set; }
    DateTimeOffset? CreatedAtGTE { get; set; }
    DateTimeOffset? CreatedAtLTE { get; set; }
}