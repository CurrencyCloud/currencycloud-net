using System;

namespace CurrencyCloud.Tests.Mock.Data
{
    static class ReportRequests
    {
        public static readonly Entity.ReportRequest Report1 = new Entity.ReportRequest
        {
            ShortReference = "RP-3934236-BZCXEW",
            ContactId = "590cea0d-0daa-48dc-882b-049107c1471f",
            Description = "Conversion test report",
            ReportUrl = "https://ccycloud-reports.example.com/011c4d705a1cf358c7ff8511f727a40784bdb078c3987bcce972d8ca76ecb505",
            SearchParams = new Entity.ReportParameters
            {
                BuyCurrency = "CAD",
                SellCurrency = "GBP",
                UniqueRequestId = "46aca410-ce74-4303-8f79-e0e95cfd9262",
                Scope = "own"
            },
            ReportType = "conversion",
            AccountId = "e277c9f9-679f-454f-8367-274b3ff977ff",
            CreatedAt = new DateTime(2018, 1, 1, 12, 34, 56),
            Id = "de5c215d-93e2-4b24-bdc8-bffbcd80c60f",
            Status = "completed",
            ExpirationDate = new DateTime(2018, 01, 31),
            UpdatedAt = new DateTime(2018, 01, 01, 12, 34, 56)
        };

        public static readonly Entity.ReportRequest Report2 = new Entity.ReportRequest
        {
            ShortReference = "RP-1224584-ARBYRB",
            ContactId = "590cea0d-0daa-48dc-882b-049107c1471f",
            Description = "Payment test report",
            ReportUrl = "https://ccycloud-reports.example.com/d12756b503cd41a9788d086bd95dece11ee3967298e59a80fca9b62c5e4edf3e",
            SearchParams = new Entity.ReportParameters
            {
                BeneficiaryId = "7b300881-8d56-44d3-afd9-6a3dfc3c295d",
                Scope = "own"
            },
            ReportType = "payment",
            AccountId = "e277c9f9-679f-454f-8367-274b3ff977ff",
            CreatedAt = new DateTime(2018, 1, 1, 12, 34, 56),
            Id = "2d71d2de-bc17-4ffb-88f4-d644b327340f",
            Status = "completed",
            ExpirationDate = new DateTime(2018, 01, 31),
            UpdatedAt = new DateTime(2018, 01, 01, 12, 34, 56)
        };

        public static readonly Entity.ReportRequest Report3 = new Entity.ReportRequest
        {
            ShortReference = "RP-9611721-CBYUBY",
            ContactId = "ca717500-c1c2-46f1-996f-5c282a4c6db4",
            Description = "New Conversion test report",
            ReportUrl = "",
            SearchParams = new Entity.ReportParameters
            {
                Scope = "own",
                UniqueRequestId = "1b3687dc-c779-4fe7-9515-00a6509632c4"
            },
            ReportType = "conversion",
            AccountId = "e277c9f9-679f-454f-8367-274b3ff977ff",
            CreatedAt = new DateTime(2018, 1, 1, 12, 34, 56),
            Id = "175b3a52-6413-4586-92d5-54f9c9d2b113",
            Status = "processing",
            FailureReason = null,
            ExpirationDate = null,
            UpdatedAt = new DateTime(2018, 01, 01, 12, 34, 56)
        };

        public static readonly Entity.ReportRequest Report4 = new Entity.ReportRequest
        {
            ShortReference = "RP-8036349-AUPHPG",
            ContactId = "ca717500-c1c2-46f1-996f-5c282a4c6db4",
            Description = "New Payment test report",
            ReportUrl = "",
            SearchParams = new Entity.ReportParameters
            {
                Scope = "own",
                UniqueRequestId = "2422a1ee-b376-4358-a4f2-560aa953c461"
            },
            ReportType = "payment",
            AccountId = "e277c9f9-679f-454f-8367-274b3ff977ff",
            CreatedAt = new DateTime(2018, 1, 1, 12, 34, 56),
            Id = "0ec2bc31-5dd8-4029-9491-6c4c0598a1cc",
            Status = "processing",
            FailureReason = null,
            ExpirationDate = null,
            UpdatedAt = new DateTime(2018, 01, 01, 12, 34, 56)
        };
    }
}