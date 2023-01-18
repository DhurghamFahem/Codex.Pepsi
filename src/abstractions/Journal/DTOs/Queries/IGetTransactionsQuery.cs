namespace Codex.Pepsi.Abstractions.Journal.DTOs.Queries;

public interface IGetTransactionsQuery
{
    Guid[]? Ids { get; set; }
    Guid? BusinessId { get; set; }
    string[]? ExternalIds { get; set; }
    DateTimeOffset? TimestampGTE { get; set; }
    DateTimeOffset? TimestampLTE { get; set; }
    DateTimeOffset? CreatedAtGTE { get; set; }
    DateTimeOffset? CreatedAtLTE { get; set; }
}
