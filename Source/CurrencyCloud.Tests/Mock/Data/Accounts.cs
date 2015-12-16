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
                YourReference = "POS-UID-23523",
                Status = "enabled",
                Street = "164 Bishops Gate",
                City = "London",
                StateOrProvince = "Ontario",
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
            YourReference = "0012345564ABC",
            Status = "enabled",
            Street = "13 London Road",
            City = "London",
            StateOrProvince = "",
            PostalCode = "AB12 CD2",
            Country = "GB",
            SpreadTable = "flat_0.5_percent",
            IdentificationType = "none"
        };
    }
}