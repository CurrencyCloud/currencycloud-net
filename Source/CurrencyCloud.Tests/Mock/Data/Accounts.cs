namespace CurrencyCloud.Tests.Mock.Data
{
    static class Accounts
    {
        public static readonly dynamic Account1 = new
        {
            AccountName = "Acme Ltd.",
            LegalEntityType = "company",

            Optional = new
            {
                YourReference = "UID-201511",
                Status = "enabled",
                Street = "194 Bishops Gate",
                City = "London",
                StateOrProvince = "",
                PostalCode = "CR4 3RZ",
                Country = "GB",
                SpreadTable = "no_markup",
                IdentificationType = "none"
            }
        };

        public static readonly dynamic Account2 = new
        {
            AccountName = "Company PLC",
            LegalEntityType = "company",

            Optional = new
            {
                YourReference = "0012345564ABC",
                Status = "enabled",
                Street = "13 London Road",
                City = "London",
                StateOrProvince = "",
                PostalCode = "AB12 CD2",
                Country = "GB",
                SpreadTable = "flat_0.5_percent",
                IdentificationType = "none"
            }
        };
    }
}