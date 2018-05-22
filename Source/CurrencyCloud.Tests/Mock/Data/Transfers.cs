namespace CurrencyCloud.Tests.Mock.Data
{
    static class Transfers
    {
        public static readonly Entity.Transfer Transfer1 = new Entity.Transfer(
            "a7117404-e150-11e6-a5af-080027a79e8f",
            "946f2d58-e150-11e6-a5af-080027a79e8f",
            "JPY",
            98765.43m)
        {
            Status = "completed",
            Reason = "Funding"
        };

        public static readonly Entity.Transfer Transfer2 = new Entity.Transfer(
            "1bd29e41-f019-0133-ed7e-0022194273c7",
            "d9c34271-b7a6-0133-9fe2-0022194273c7",
            "EUR",
            1234.56m)
        {
            Status = "completed"
        };

        public static readonly Entity.Transfer Transfer3 = new Entity.Transfer(
            "a7117404-e150-11e6-a5af-080027a79e8f",
            "946f2d58-e150-11e6-a5af-080027a79e8f",
            "GBP",
            1250m)
        {
            Status = "completed",
            Reason = "Funding"
        };
    }
}