using System.Security.Principal;

namespace Codex.Pepsi.Implementation.Accounts;

public class Account : IAccount
{
    public Guid Id { get; set; }
    public Guid BusinessId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string? ExternalId { get; set; }
    public Guid? ParentId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string? Note { get; set; }
    public Dictionary<string, decimal> Credits { get; set; } = new();
    public Dictionary<string, decimal> Debits { get; set; } = new();
    public Dictionary<string, decimal> Balances
    {
        get
        {
            var res = new Dictionary<string, decimal>();
            foreach (var (key, value) in Credits)
            {
                res[key] = value;
            }
            foreach (var (key, value) in Debits)
            {
                if (res.ContainsKey(key))
                {
                    res[key] -= value;
                }
                else
                {
                    res[key] = -value;
                }
            }
            return res;
        }
    }

    public void Append(IJournalEntry journalEntry)
    {
        var dict = journalEntry.Type == IJournalEntry.JournalEntryType.Credit ? Credits : Debits;
        if (dict.ContainsKey(journalEntry.Money.Currency))
        {
            dict[journalEntry.Money.Currency] += journalEntry.Money.Amount;
        }
        else
        {
            dict.Add(journalEntry.Money.Currency, journalEntry.Money.Amount);
        }
    }
}
