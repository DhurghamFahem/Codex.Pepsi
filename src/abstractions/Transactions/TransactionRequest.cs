namespace Codex.Pepsi.Abstractions.Transactions;

public abstract class TransactionRequest<TRequest> where TRequest : TransactionRequest<TRequest>
{
    public Guid BusinessId { get; set; }
    public string? ExternalId { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public string? Note { get; set; }
}
