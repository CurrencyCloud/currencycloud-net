using System;

namespace CurrencyCloud.Tests.Mock.Data
{
    static class Payments
    {
        public static readonly Entity.Payment Payment1 = new Entity.Payment(

            "EUR",
            "To be filled later",
            10000,
            "Salary for March",
            "INVOICE 9876"
            )
        {
            PaymentType = "regular"
        };
        public static readonly Entity.Payer Payer1 = new Entity.Payer()
        { 
            LegalEntityType = "individual",
            CompanyName = "Some Company LLC",
            FirstName = "John",
            LastName = "Doe",
            City = "London",
            Address = "27 Colmore Row",
            Postcode = "W11 2BQ",
            StateOrProvince = "TX",
            Country = "GB",
            DateOfBirth = new DateTime(1980, 10, 10),
            IdentificationType = "none"
        };

        public static readonly Entity.Payment Payment2 = new Entity.Payment(
            "EUR",
            "To be filled later",
            0.23m,
            "Prepayment of salary for April",
            "INVOICE 122"
            )
        {
            PaymentType = "regular"
        };

        public static readonly Entity.Payer Payer2 = new Entity.Payer()
        { 
            LegalEntityType = "individual",
            CompanyName = "Jens enskild firma",
            FirstName = "Jennifer",
            LastName = "Waylon",
            City = "Stockholm",
            Address = "22 Garvargatan",
            Postcode = "12332",
            StateOrProvince = "Stockholm",
            Country = "SE",
            DateOfBirth = new DateTime(1981, 12, 10),
            IdentificationType = "none"
        };
    }
}