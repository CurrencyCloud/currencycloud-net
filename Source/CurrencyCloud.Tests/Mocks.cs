using CurrencyCloud.Environment;

namespace CurrencyCloud.Tests
{
    static class Mocks
    {
        public static readonly dynamic Credentials = new
        {
            ApiServer = ApiServer.Demo,
            LoginId = "test.it@mailinator.com",
            APIkey = "b5266326b1855443544626f188b8a234da99e1c36d91819419e17091b4f0a7f4"
        };

        public static readonly dynamic Account1 = new
        {
            AccountName = "Vive",
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
