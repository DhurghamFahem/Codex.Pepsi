namespace Codex.Pepsi.Abstractions.Monetary;

public static class CurrencyUtility
{
    public static bool IsValidCurrency(this string currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
            return false;
        return _knownCurrencies.Contains(currency.ToUpper());
    }

    #region Known Currencies
    private static readonly HashSet<string> _knownCurrencies = new()
    {
        "AED",
        "AFN",
        "ALL",
        "AMD",
        "ARS",
        "AUD",
        "AZN",
        "BAM",
        "BDT",
        "BGN",
        "BHD",
        "BND",
        "BOB",
        "BRL",
        "BTN",
        "BWP",
        "BYN",
        "BZD",
        "CAD",
        "CDF",
        "CHF",
        "CLP",
        "CNY",
        "COP",
        "CRC",
        "CUP",
        "CZK",
        "DKK",
        "DOP",
        "DZD",
        "EGP",
        "ERN",
        "ETB",
        "EUR",
        "GBP",
        "GEL",
        "GTQ",
        "HKD",
        "HNL",
        "HRK",
        "HTG",
        "HUF",
        "IDR",
        "ILS",
        "INR",
        "IQD",
        "IRR",
        "ISK",
        "JMD",
        "JOD",
        "JPY",
        "KES",
        "KGS",
        "KHR",
        "KRW",
        "KWD",
        "KZT",
        "LAK",
        "LBP",
        "LKR",
        "LYD",
        "MAD",
        "MDL",
        "MKD",
        "MMK",
        "MNT",
        "MVR",
        "MXN",
        "MYR",
        "NGN",
        "NIO",
        "NOK",
        "NPR",
        "NZD",
        "OMR",
        "PAB",
        "PEN",
        "PHP",
        "PKR",
        "PLN",
        "PYG",
        "QAR",
        "RON",
        "RSD",
        "RUB",
        "RWF",
        "SAR",
        "SEK",
        "SGD",
        "SOS",
        "SYP",
        "THB",
        "TMT",
        "TND",
        "TRY",
        "TTD",
        "UAH",
        "USD",
        "UYU",
        "UZS",
        "VES",
        "VND",
        "XAF",
        "XOF",
        "YER",
        "ZAR",
    };
    #endregion
}
