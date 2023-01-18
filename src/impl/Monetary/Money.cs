using Codex.Pepsi.Abstractions.Monetary;

namespace Codex.Pepsi.Implementation.Monetary;

public sealed partial class Money : IMoney
{
    public string Currency { get; }
    public decimal Amount { get; }

    public Money(string currency, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentNullException(nameof(currency), "Currency cannot be null or empty");
        var trimmedUppedCurrency = currency.Trim().ToUpper();
        if (!trimmedUppedCurrency.IsValidCurrency())
        {
            throw new ArgumentOutOfRangeException(nameof(currency), $"{nameof(currency)}'s value ({currency}) is not a valid currency");
        }

        Currency = trimmedUppedCurrency;
        Amount = amount;
    }

    public IMoney Exchange(IMoney exchangeRate)
        => new Money(exchangeRate.Currency, Amount * exchangeRate.Amount);

    public override string ToString()
        => $"{Amount} {Currency}";

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        if (obj is Money money)
            return Currency == money.Currency && Math.Abs(Amount - money.Amount) <= (decimal)double.Epsilon;
        return false;
    }

    public override int GetHashCode()
        => Currency.GetHashCode() ^ Amount.GetHashCode();
}