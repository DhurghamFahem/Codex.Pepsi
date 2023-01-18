using Codex.Pepsi.Abstractions.Journal.DTOs.Queries;
using Codex.Pepsi.Abstractions.Transactions;

namespace Codex.Pepsi.Abstractions.Journal;

public interface IJournal
{
    void Append<TTransaction>(TTransaction transaction) where TTransaction : class, ITransaction;
    Task AppendAsync<TTransaction>(TTransaction transaction) where TTransaction : class, ITransaction;
    IJournalEntry[] GetEntries(Guid transactionId);
    Task<IJournalEntry[]> GetEntriesAsync(Guid transactionId);
    IJournalEntry[] GetEntries(IGetEntriesQuery getEntriesQuery);
    Task<IJournalEntry[]> GetEntriesAsync(IGetEntriesQuery getEntriesQuery);
    Task<TTransaction[]> GetTransactionsAsync<TTransaction>(IGetTransactionsQuery query) where TTransaction : class, ITransaction;
    TTransaction[] GetTransactions<TTransaction>(IGetTransactionsQuery query) where TTransaction : class, ITransaction;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}
