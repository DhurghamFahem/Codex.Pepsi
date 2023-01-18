namespace Codex.Pepsi.Implementation.Monetary.Extensions;

public static class MoneyExtensions
{
    public static Money ToMoney(this decimal amount, string currency)
       => new(currency, amount);

    public static Money ToMoney(this double amount, string currency)
        => new(currency, Convert.ToDecimal(amount));

    public static Money ToMoney(this long amount, string currency)
        => new(currency, Convert.ToDecimal(amount));

    public static Money ToMoney(this int amount, string currency)
        => new(currency, Convert.ToDecimal(amount));
}
