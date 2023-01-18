using Codex.Pepsi.Abstractions.Journal;
using Codex.Pepsi.Abstractions.Monetary;

namespace Codex.Pepsi.Implementation.Journal;

public class JournalEntry : IJournalEntry
{
    public Guid Id { get; init; }
    public Guid TransactionId { get; init; }
    public string? ExternalId { get; set; }
    public DateTimeOffset Timestamp { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public Guid AccountId { get; init; }
    public string? Note { get; set; }
    public long Serial { get; set; }
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public IMoney Money { get; init; }
    public IJournalEntry.JournalEntryType Type { get; init; }
#pragma warning restore CS8618

    public virtual bool IsValid()
    {
        var res = true;
        res &= Money is not null && Money.Amount > 0;
        res &= Id != Guid.Empty;
        res &= TransactionId != Guid.Empty;
        res &= AccountId != Guid.Empty;
        res &= Timestamp != DateTimeOffset.MinValue && Timestamp != DateTimeOffset.MaxValue;
        res &= CreatedAt != DateTimeOffset.MinValue && CreatedAt != DateTimeOffset.MaxValue;
        return res;
    }
}
