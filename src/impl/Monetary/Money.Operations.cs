namespace Codex.Pepsi.Implementation.Monetary;

public sealed partial class Money
{
    public static bool operator ==(Money? left, Money? right)
    {
        if (left is null || right is null)
            return false;
        return left.Currency == right.Currency && Math.Abs(left.Amount - right.Amount) <= (decimal)double.Epsilon;
    }

    public static bool operator !=(Money? left, Money? right)
    {
        if (left is null || right is null)
            return true;
        return left.Currency != right.Currency || Math.Abs(left.Amount - right.Amount) > (decimal)double.Epsilon;
    }

    public static bool operator ==(Money left, decimal right)
        => Math.Abs(left.Amount - right) <= (decimal)double.Epsilon;

    public static bool operator !=(Money left, decimal right)
        => Math.Abs(left.Amount - right) > (decimal)double.Epsilon;

    public static Money operator +(Money a, Money b)
    {
        if (a.Currency != b.Currency)
        {
            throw new ArgumentException("Cannot add two money objects with different currencies");
        }
        return new Money(a.Currency, a.Amount + b.Amount);
    }

    public static Money operator +(Money a, decimal b)
        => new(a.Currency, a.Amount + b);

    public static Money operator +(Money a, int b)
        => new(a.Currency, a.Amount + b);

    public static Money operator +(Money a)
        => new(a.Currency, +a.Amount);

    public static Money operator -(Money a, Money b)
    {
        if (a.Currency != b.Currency)
        {
            throw new ArgumentException("Cannot subtract two money objects with different currencies");
        }
        return new Money(a.Currency, a.Amount - b.Amount);
    }

    public static Money operator -(Money a)
        => new(a.Currency, -a.Amount);

    public static Money operator -(Money a, decimal b)
        => new(a.Currency, a.Amount - b);

    public static Money operator *(Money a, Money b)
    {
        if (a.Currency != b.Currency)
        {
            throw new ArgumentException("Cannot multiply two money objects with different currencies");
        }
        return new Money(a.Currency, a.Amount * b.Amount);
    }

    public static Money operator *(Money a, decimal b)
        => new(a.Currency, a.Amount * b);

    public static Money operator /(Money a, Money b)
    {
        if (a.Currency != b.Currency)
        {
            throw new ArgumentException("Cannot divide two money objects with different currencies");
        }
        return new Money(a.Currency, a.Amount / b.Amount);
    }

    public static Money operator /(Money a, decimal b)
        => new(a.Currency, a.Amount / b);

    public static bool operator >(Money a, Money b)
    {
        if (a.Currency != b.Currency)
        {
            throw new ArgumentException("Cannot compare two money objects with different currencies");
        }
        return a.Amount > b.Amount;
    }

    public static bool operator <(Money a, Money b)
    {
        if (a.Currency != b.Currency)
        {
            throw new ArgumentException("Cannot compare two money objects with different currencies");
        }
        return a.Amount < b.Amount;
    }

    public static bool operator >(Money a, decimal b)
        => a.Amount > b;

    public static bool operator <(Money a, decimal b)
        => a.Amount < b;
}
