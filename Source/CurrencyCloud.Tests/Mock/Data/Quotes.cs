namespace CurrencyCloud.Tests.Mock.Data
{
    static class Quotes
    {
        public static readonly Entity.Quote Quote1 = new Entity.Quote(
            "USD",
            "EUR",
            "sell",
            100.00m,
            "3m"
        );
    }
}
