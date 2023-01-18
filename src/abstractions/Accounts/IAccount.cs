using Codex.Pepsi.Abstractions.Journal;

namespace Codex.Pepsi.Abstractions.Accounts;

public interface IAccount
{
    Guid Id { get; set; }
    Guid BusinessId { get; set; }
    string Number { get; set; }
    string? ExternalId { get; set; }
    string Name { get; set; }
    Guid? ParentId { get; set; }
    Dictionary<string, decimal> Credits { get; set; }
    Dictionary<string, decimal> Debits { get; set; }
    Dictionary<string, decimal> Balances { get; }
    DateTimeOffset CreatedAt { get; set; }
    string? Note { get; set; }

    void Append(IJournalEntry journalEntry);
}
