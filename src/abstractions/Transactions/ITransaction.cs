using Codex.Pepsi.Abstractions.Journal;

namespace Codex.Pepsi.Abstractions.Transactions;

public interface ITransaction
{
    Guid Id { get; init; }
    Guid BusinessId { get; set; }
    string? ExternalId { get; set; }
    DateTimeOffset Timestamp { get; init; }
    DateTimeOffset CreatedAt { get; init; }
    IReadOnlyList<IJournalEntry> Entries { get; }
    public string? Note { get; set; }

    void Process<TRequest>(TRequest request) where TRequest : TransactionRequest<TRequest>;
    void Append(IJournalEntry journalEntry);
    bool IsValid();
    bool IsBalanced();
}

public interface ITransaction<TRequest> : ITransaction
    where TRequest : TransactionRequest<TRequest>
{
    void Process(TRequest request);
}

public interface ITransaction<TRequest, TRequest2> : ITransaction<TRequest>
    where TRequest : TransactionRequest<TRequest>
    where TRequest2 : TransactionRequest<TRequest2>
{
    void Process(TRequest2 request);
}

public interface ITransaction<TRequest, TRequest2, TRequest3> : ITransaction<TRequest, TRequest2>
    where TRequest : TransactionRequest<TRequest>
    where TRequest2 : TransactionRequest<TRequest2>
    where TRequest3 : TransactionRequest<TRequest3>
{
    void Process(TRequest3 request);
}

public interface ITransaction<TRequest, TRequest2, TRequest3, TRequest4> : ITransaction<TRequest, TRequest2, TRequest3>
    where TRequest : TransactionRequest<TRequest>
    where TRequest2 : TransactionRequest<TRequest2>
    where TRequest3 : TransactionRequest<TRequest3>
    where TRequest4 : TransactionRequest<TRequest4>
{
    void Process(TRequest4 request);
}
