namespace CurrencyCloud.Tests.Mock.Data
{
    static class Conversions
    {
        public static readonly Entity.Conversion Conversion1 = new Entity.Conversion(
            "EUR",
            "GBP",
            "buy",
            10000.23m,
            true
        );
    }
}
