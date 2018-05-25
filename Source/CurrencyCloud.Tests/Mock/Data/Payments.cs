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

        public static readonly Entity.PaymentSubmission Submission1 = new Entity.PaymentSubmission
        {
            Status = "pending",
            Mt103 = "{1:F01TCCLGB20AXXX0090000004}{2:I103BARCGB22XXXXN}{4: :20:20180101-ZSYWVY :23B:CRED :32A:160617GBP3000,0 :33B:GBP3000,0 :50K:/150618-00026 PCOMAPNY address New-York Province 555222 GB :53B:/20060513071472 :57C://SC200605 :59:/200605000 First Name Last Name e03036bf6c325dd12c58 London GB :70:test reference Test reason Payment group: 0160617-ZSYWVY :71A:SHA -}",
            SubmissionRef = "MXGGYAGJULIIQKDV"
        };
    }
}
