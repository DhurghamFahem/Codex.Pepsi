namespace Codex.Pepsi.Abstractions.Monetary;

public interface IMoney
{
    string Currency { get; }
    decimal Amount { get; }

    IMoney Exchange(IMoney exchangeRate);
}
