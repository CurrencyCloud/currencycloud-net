namespace CurrencyCloud.Tests.Mock.Data
{
    static class Accounts
    {
        public static readonly Entity.Account Account1 = new Entity.Account(
            "Acme Ltd.",
            "company",
            "12 Steward St",
            "London",
            "E1 6FQ",
            "GB")
            {
                YourReference = "POS-UID-23523",
                Status = "enabled",
                StateOrProvince = "London",
                SpreadTable = "no_markup",
                IdentificationType = "none",
                Brand = "currencycloud",
                ApiTrading = true,
                OnlineTrading = true,
                PhoneTrading = true
        };

        public static readonly Entity.Account Account2 = new Entity.Account(
            "Company PLC",
            "company",
            "12 Steward St",
            "London",
            "E1 6FQ",
            "GB")
        {
            YourReference = "0012345564ABC",
            Status = "enabled",
            StateOrProvince = "",
            SpreadTable = "flat_0.5_percent",
            IdentificationType = "none",
            Brand = "currencycloud",
            ApiTrading = true,
            OnlineTrading = true,
            PhoneTrading = true
        };
    }
}
