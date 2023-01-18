using Codex.Pepsi.Abstractions.Journal;
using Codex.Pepsi.Abstractions.Transactions;

namespace Codex.Pepsi.Implementation.Transactions;

public abstract class Transaction : ITransaction
{
    public Guid BusinessId { get; set; }
    private readonly List<IJournalEntry> _entries = new();
    private readonly Dictionary<string, decimal> _diffByCurrency = new();

    public Guid Id { get; init; }

    /// <summary>
    /// External ID of the transaction.
    /// <para>Optional. For example, a transaction ID from a bank statement.</para>
    /// </summary>
    public string? ExternalId { get; set; }

    /// <summary>
    /// Timestamp of the transaction.
    // </summary>
    public DateTimeOffset Timestamp { get; init; }

    /// <summary>
    /// Timestamp of the transaction creation.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }
    public IReadOnlyList<IJournalEntry> Entries => _entries;

    public string? Note { get; set; }

    /// <summary>
    /// Initializing a transaction with a new Id, timestamp; and createdat set to now and timezone set to UTC.
    /// </summary>
    protected Transaction()
    {
        Id = Guid.NewGuid();
        var now = DateTimeOffset.UtcNow;
        Timestamp = now;
        CreatedAt = now;
    }

    /// <summary>
    /// Initializing a transaction with a new Id, timestamp, and timezone to the given values.
    /// <param name="timestamp">The timestamp of the transaction.</param>
    /// <param name="createdAt">The creation timestamp of the transaction.</param>
    /// <para />
    /// <exception cref="ArgumentNullException">Thrown if timestamp is default, min or max.</exception>
    /// </summary>
    protected Transaction(DateTimeOffset timestamp, DateTimeOffset createdAt) : this()
    {
        if (timestamp == default)
            throw new ArgumentNullException(nameof(timestamp), "Timestamp cannot be null");
        if (timestamp == DateTimeOffset.MinValue || timestamp == DateTimeOffset.MaxValue)
            throw new ArgumentException("Timestamp cannot be MinValue or MaxValue", nameof(timestamp));
        if (createdAt == default)
            throw new ArgumentNullException(nameof(createdAt), "CreatedAt cannot be null");
        if (createdAt == DateTimeOffset.MinValue || createdAt == DateTimeOffset.MaxValue)
            throw new ArgumentException("CreatedAt cannot be MinValue or MaxValue", nameof(createdAt));
        Timestamp = timestamp;
        CreatedAt = createdAt;
    }

    /// <summary>
    /// Initializing a transaction with a new Id, timestamp, and timezone to the given values.
    /// <param name="id">The Id.</param>
    /// <param name="timestamp">The timestamp of the transaction.</param>
    /// <param name="createdAt">The creation timestamp of the transaction.</param>
    /// <para />
    /// <exception cref="ArgumentNullException">Thrown if timestamp is default, min or max.</exception>
    /// <exception cref="ArgumentException">Thrown if id is an empty/zero GUID/UUID or the timestamp is default, min or max.</exception>
    /// </summary>
    protected Transaction(Guid id, DateTimeOffset timestamp, DateTimeOffset createdAt) : this(timestamp, createdAt)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty", nameof(id));
        Id = id;
    }

    /// <summary>
    /// Appends a journal entry to the transaction.
    /// <param name="journalEntry">The journal entry to append.</param>
    /// <para />
    /// <exception cref="ArgumentNullException">Thrown if journalEntry is null.</exception>
    /// <exception cref="ArgumentException">Thrown if journalEntry is not valid.</exception>
    /// </summary>
    public void Append(IJournalEntry journalEntry)
    {
        if (journalEntry is null)
            throw new ArgumentNullException(nameof(journalEntry), "Journal entry cannot be null");
        if (!journalEntry.IsValid())
            throw new ArgumentException("Journal entry is not valid", nameof(journalEntry));
        _entries.Add(journalEntry);
        if (!_diffByCurrency.ContainsKey(journalEntry.Money.Currency))
        {
            _diffByCurrency.Add(journalEntry.Money.Currency, 0);
        }
        _diffByCurrency[journalEntry.Money.Currency] += (int)journalEntry.Type * journalEntry.Money.Amount;
    }

    /// <summary>
    /// Checks if the transaction is valid.
    /// </summary>
    public virtual bool IsValid()
    {
        var res = true;
        res &= IsBalanced();
        res &= AreAllEntriesValid();
        return res;
    }

    /// <summary>
    /// Checks if the transaction is balanced. I.e., if the total of all debits equals the total of all credits.
    /// </summary>
    public virtual bool IsBalanced()
        => _diffByCurrency.Values.All(x => x == 0);

    /// <summary>
    /// Checks if all credit and debit journal entries are valid.
    /// </summary>
    protected virtual bool AreAllEntriesValid()
    {
        var res = true;
        res &= _entries.Count > 1;
        res &= _entries.All(x => x.IsValid());
        return res;
    }

    public abstract void Process<TRequest>(TRequest request) where TRequest : TransactionRequest<TRequest>;
}

public abstract class Transaction<TRequest1> : Transaction, ITransaction<TRequest1> where TRequest1 : TransactionRequest<TRequest1>
{
    protected Transaction()
    {
    }

    protected Transaction(DateTimeOffset timestamp, DateTimeOffset createdAt) : base(timestamp, createdAt)
    {
    }

    protected Transaction(Guid id, DateTimeOffset timestamp, DateTimeOffset createdAt) : base(id, timestamp, createdAt)
    {
    }

    public abstract void Process(TRequest1 request);

    public override void Process<TRequest>(TRequest request)
    {
        BusinessId = request.BusinessId;
        if (request is TRequest1 request1)
        {
            Process(request1);
        }
        else
        {
            throw new NotImplementedException();
        }
    }
}

public abstract class Transaction<TRequest1, TRequest2> : Transaction<TRequest1>, ITransaction<TRequest1, TRequest2>
    where TRequest1 : TransactionRequest<TRequest1>
    where TRequest2 : TransactionRequest<TRequest2>
{
    protected Transaction()
    {
    }

    protected Transaction(DateTimeOffset timestamp, DateTimeOffset createdAt) : base(timestamp, createdAt)
    {
    }

    protected Transaction(Guid id, DateTimeOffset timestamp, DateTimeOffset createdAt) : base(id, timestamp, createdAt)
    {
    }

    public abstract void Process(TRequest2 request);

    public override void Process<TRequest>(TRequest request)
    {
        BusinessId = request.BusinessId;
        if (request is TRequest1 request1)
        {
            Process(request1);
        }
        else if (request is TRequest2 request2)
        {
            Process(request2);
        }
        else
        {
            throw new NotImplementedException();
        }
    }
}

public abstract class Transaction<TRequest1, TRequest2, TRequest3> : Transaction<TRequest1, TRequest2>, ITransaction<TRequest1, TRequest2, TRequest3>
    where TRequest1 : TransactionRequest<TRequest1>
    where TRequest2 : TransactionRequest<TRequest2>
    where TRequest3 : TransactionRequest<TRequest3>
{
    protected Transaction()
    {
    }

    protected Transaction(DateTimeOffset timestamp, DateTimeOffset createdAt) : base(timestamp, createdAt)
    {
    }

    protected Transaction(Guid id, DateTimeOffset timestamp, DateTimeOffset createdAt) : base(id, timestamp, createdAt)
    {
    }

    public abstract void Process(TRequest3 request);

    public override void Process<TRequest>(TRequest request)
    {
        BusinessId = request.BusinessId;
        if (request is TRequest1 request1)
        {
            Process(request1);
        }
        else if (request is TRequest2 request2)
        {
            Process(request2);
        }
        else if (request is TRequest3 request3)
        {
            Process(request3);
        }
        else
        {
            throw new NotImplementedException();
        }
    }
}

public abstract class Transaction<TRequest1, TRequest2, TRequest3, TRequest4> : Transaction<TRequest1, TRequest2, TRequest3>, ITransaction<TRequest1, TRequest2, TRequest3, TRequest4>
    where TRequest1 : TransactionRequest<TRequest1>
    where TRequest2 : TransactionRequest<TRequest2>
    where TRequest3 : TransactionRequest<TRequest3>
    where TRequest4 : TransactionRequest<TRequest4>
{
    protected Transaction()
    {
    }

    protected Transaction(DateTimeOffset timestamp, DateTimeOffset createdAt) : base(timestamp, createdAt)
    {
    }

    protected Transaction(Guid id, DateTimeOffset timestamp, DateTimeOffset createdAt) : base(id, timestamp, createdAt)
    {
    }

    public abstract void Process(TRequest4 request);

    public override void Process<TRequest>(TRequest request)
    {
        BusinessId = request.BusinessId;
        if (request is TRequest1 request1)
        {
            Process(request1);
        }
        else if (request is TRequest2 request2)
        {
            Process(request2);
        }
        else if (request is TRequest3 request3)
        {
            Process(request3);
        }
        else if (request is TRequest4 request4)
        {
            Process(request4);
        }
        else
        {
            throw new NotImplementedException();
        }
    }
}
