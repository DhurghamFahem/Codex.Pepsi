using Codex.Pepsi.Abstractions.Monetary;

namespace Codex.Pepsi.Abstractions.Journal;

public interface IJournalEntry
{
    Guid Id { get; init; }
    Guid TransactionId { get; init; }
    string? ExternalId { get; set; }
    DateTimeOffset Timestamp { get; init; }
    DateTimeOffset CreatedAt { get; init; }
    Guid AccountId { get; init; }
    IMoney Money { get; init; }
    JournalEntryType Type { get; init; }
    string? Note { get; set; }
    long Serial { get; set; }

    bool IsValid();

    public enum JournalEntryType
    {
        Debit = -1,
        Credit = 1,
    }
}
