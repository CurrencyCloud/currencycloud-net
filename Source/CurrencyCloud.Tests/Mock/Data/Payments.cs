using System;

namespace CurrencyCloud.Tests.Mock.Data
{
    static class Payments
    {
        public static dynamic Payment1 = new
        {
            Currency = "EUR",
            Amount = 10000,
            Reason = "Salary for March",
            Reference = "INVOICE 9876",

            Optional = new
            {
                PaymentType = "regular",
                PayerEntityType = "individual",
                PayerCompanyName = "Some Company LLC",
                PayerFirstName = "John",
                PayerLastName = "Doe",
                PayerCity = "London",
                PayerAddress = "27 Colmore Row",
                PayerPostcode = "W11 2BQ",
                PayerStateOrProvince = "TX",
                PayerCountry = "GB",
                PayerDateOfBirth = new DateTime(1980, 10, 10),
                PayerIdentificationType = "none"
            }
        };

        public static readonly dynamic Payment2 = new
        {
            Currency = "EUR",
            Amount = 0.23m,
            Reason = "Prepayment of salary for April",
            Reference = "INVOICE 122",
            PaymentType = "regular",
            PayerEntityType = "individual",
            PayerCompanyName = "Jens enskild firma",
            PayerFirstName = "Jennifer",
            PayerLastName = "Waylon",
            PayerCity = "Stockholm",
            PayerAddress = "22 Garvargatan",
            PayerPostcode = "12332",
            PayerStateOrProvince = "Stockholm",
            PayerCountry = "SE",
            PayerDateOfBirth = new DateTime(1981, 12, 10),
            PayerIdentificationType = "none"
        };
    }
}