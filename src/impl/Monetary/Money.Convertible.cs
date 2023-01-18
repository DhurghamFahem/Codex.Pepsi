namespace Codex.Pepsi.Implementation.Monetary;

public sealed partial class Money
{
    public static explicit operator decimal(Money money)
        => money.Amount;

    public static explicit operator double(Money money)
        => Convert.ToDouble(money.Amount);

    public static explicit operator float(Money money)
        => (float)Convert.ToDouble(money.Amount);

    public static explicit operator long(Money money)
        => Convert.ToInt64(money.Amount);

    public static explicit operator int(Money money)
        => Convert.ToInt32(money.Amount);

    public static explicit operator string(Money money)
        => money.ToString();

    public static explicit operator Money(decimal amount)
        => new("USD", amount);

    public static explicit operator Money(double amount)
        => new("USD", Convert.ToDecimal(amount));

    public static explicit operator Money(float amount)
        => new("USD", Convert.ToDecimal(amount));

    public static explicit operator Money(long amount)
        => new("USD", Convert.ToDecimal(amount));

    public static explicit operator Money(int amount)
        => new("USD", Convert.ToDecimal(amount));

    public static explicit operator Money(string amount)
        => new("USD", Convert.ToDecimal(amount));
}
