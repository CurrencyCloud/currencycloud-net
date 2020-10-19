using System;
using System.Collections.Generic;
using CurrencyCloud.Entity;

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
        public static readonly Entity.Payer Payer1 = new Entity.Payer
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

        public static readonly Entity.Payer Payer2 = new Entity.Payer
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

        public static readonly Entity.List.PaymentAuthorisationsList.Authorisation Authorisation1 =
            new Entity.List.PaymentAuthorisationsList.Authorisation
        {
            PaymentId = "8e3aeeb8-deeb-4665-96de-54b880a953ac",
            PaymentStatus = "authorised",
            Updated = false,
            Error = "",
            AuthStepsTaken = 1,
            AuthStepsRequired = 1,
            ShortReference = "180802-YKGDVV001"
        };

        public static readonly Entity.List.PaymentAuthorisationsList.Authorisation Authorisation2 =
            new Entity.List.PaymentAuthorisationsList.Authorisation
        {
            PaymentId = "f16cafe4-1f8f-472e-99d9-8c828918d4f8",
            PaymentStatus = "authorised",
            Updated = true,
            Error = "",
            AuthStepsTaken = 1,
            AuthStepsRequired = 0,
            ShortReference = "180802-BXXTBP001"
        };

        public static readonly Entity.List.PaymentAuthorisationsList.Authorisation Authorisation3 =
            new Entity.List.PaymentAuthorisationsList.Authorisation
        {
            PaymentId = "d025f90f-a23c-46f9-979a-35a9f98d9491",
            PaymentStatus = "authorised",
            Updated = false,
            Error = "",
            AuthStepsTaken = 1,
            AuthStepsRequired = 1,
            ShortReference = "180802-ZVTTLF001"
        };

        public static readonly Entity.PaymentConfirmation Confirmation1 = new Entity.PaymentConfirmation
        {
            Id = "e6b30f2d-0088-4d99-bb47-c6b136fcf447",
            PaymentId = "855fa573-1ace-4da2-a55b-912f10103055",
            AccountId = "72970a7c-7921-431c-b95f-3438724ba16f",
            ShortReference = "PC-2436231-LYODVS",
            Status = "completed",
            ConfirmationUrl = "https://ccycloud-reports.example.com/payment_confirmations/404b407a-d143-4497-b5b9-6eb856377e20",
            CreatedAt = new DateTime(2018, 01, 01, 12, 34, 56),
            UpdatedAt = new DateTime(2018, 01, 01, 12, 34, 56),
            ExpiresAt = new DateTime(2018, 01, 03)
        };

        public static readonly PaymentTrackingInfo TrackingInfo1 = new Entity.PaymentTrackingInfo
        {
            Uetr = "46ed4827-7b6f-4491-a06f-b548d5a7512d",
            InitiationTime = DateTime.Parse("2019-07-09T13:20:30+00:00"),
            LastUpdateTime = DateTime.Parse("2019-07-10T15:39:08+00:00")
        };
    }
}
