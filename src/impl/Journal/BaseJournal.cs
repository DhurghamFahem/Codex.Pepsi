using Codex.Pepsi.Abstractions.Accounts;
using Codex.Pepsi.Abstractions.Journal;
using Codex.Pepsi.Abstractions.Journal.DTOs.Queries;
using Codex.Pepsi.Abstractions.Transactions;
using Codex.Pepsi.Abstractions.Transactions.Exceptions;

namespace Codex.Pepsi.Implementation.Journal;

public abstract class BaseJournal : IJournal
{
    protected readonly IChartOfAccounts ChartOfAccounts;

    protected BaseJournal(IChartOfAccounts chartOfAccounts)
    {
        ChartOfAccounts = chartOfAccounts;
    }

    public virtual void Append<TTransaction>(TTransaction transaction) where TTransaction : class, ITransaction
    {
        if (!transaction.IsValid())
            throw new InvalidTransactionException("E001: Transaction is not valid");
        AppendValidated(transaction);
        var accountIds = transaction.Entries.Select(x => x.AccountId).Distinct();
        var accounts = ChartOfAccounts.GetAccounts(accountIds);
        foreach (var entry in transaction.Entries)
            accounts[entry.AccountId].Append(entry);
        ChartOfAccounts.UpdateRange(accounts.Values);
    }

    public virtual async Task AppendAsync<TTransaction>(TTransaction transaction) where TTransaction : class, ITransaction
    {
        if (!transaction.IsValid())
            throw new InvalidTransactionException("E001: Transaction is not valid");
        await AppendValidatedAsync(transaction);
        var accountIds = transaction.Entries.Select(x => x.AccountId).Distinct();
        var accounts = await ChartOfAccounts.GetAccountsAsync(accountIds);
        foreach (var entry in transaction.Entries)
            accounts[entry.AccountId].Append(entry);
        await ChartOfAccounts.UpdateRangeAsync(accounts.Values);
    }

    public abstract IJournalEntry[] GetEntries(Guid transactionId);
    public abstract Task<IJournalEntry[]> GetEntriesAsync(Guid transactionId);
    public abstract IJournalEntry[] GetEntries(IGetEntriesQuery getEntriesQuery);
    public abstract Task<IJournalEntry[]> GetEntriesAsync(IGetEntriesQuery getEntriesQuery);
    public abstract TTransaction[] GetTransactions<TTransaction>(IGetTransactionsQuery query) where TTransaction : class, ITransaction;
    public abstract Task<TTransaction[]> GetTransactionsAsync<TTransaction>(IGetTransactionsQuery query) where TTransaction : class, ITransaction;
    public abstract Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public abstract int SaveChanges();

    protected abstract void AppendValidated<TTransaction>(TTransaction transaction) where TTransaction : class, ITransaction;
    protected abstract Task AppendValidatedAsync<TTransaction>(TTransaction transaction) where TTransaction : class, ITransaction;
}
