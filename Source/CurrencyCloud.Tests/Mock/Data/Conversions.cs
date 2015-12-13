namespace CurrencyCloud.Tests.Mock.Data
{
    static class Conversions
    {
        public static readonly dynamic Conversion1 = new
        {
            BuyCurrency = "EUR",
            SellCurrency = "GBP",
            FixedSide = "buy",
            Amount = 10000.23,
            Reason = "Settling invoices",
            TermAgreement = true
        };
    }
}