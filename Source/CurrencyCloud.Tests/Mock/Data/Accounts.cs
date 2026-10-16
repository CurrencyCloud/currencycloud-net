using System;
using System.Collections.Generic;

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
                PhoneTrading = true,
                TermsAndConditionsAccepted = true
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

        public static readonly Entity.AccountCreateRequest AccountCreateRequest = new Entity.AccountCreateRequest(
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
            TermsAndConditionsAccepted = true,
            Brand = "currencycloud",
            ApiTrading = true,
            OnlineTrading = true,
            PhoneTrading = true,
            LegalEntitySubType = "limited_liability_company",
            IdentificationIssuer = "US",
            IdentificationExpiration = new DateOnly(2100, 2, 2)
        };


        public static readonly Entity.PaymentChargesSettings PaymentCharges = new Entity.PaymentChargesSettings(
            "e277c9f9-679f-454f-8367-274b3ff977ff",
            "18f3f814-fef0-4211-a028-fe22c4b69818")
        {
            Enabled = true,
            Default = true
        };

        public static readonly Entity.AccountComplianceSettings ComplianceSettings = new Entity.AccountComplianceSettings(
            "e277c9f9-679f-454f-8367-274b3ff977ff")

        {
            IndustryType = "some-type",
            CountryOfIncorporation = "US",
            DateOfIncorporation = new DateOnly(2020, 1, 30),
            BusinessWebsiteUrl = "https://currencycloud.com",
            ExpectedTransactionCurrencies = new List<string> { "GBP" },
            ExpectedTransactionCountries = new List<string> { "US", "GB" },
            ExpectedMonthlyActivityVolume = 10,
            ExpectedMonthlyActivityValue = 30.00m,
            TaxIdentification = "some-tax-id",
            NationalIdentification = "some-national-id",
            CountryOfCitizenship = "US",
            TradingAddressStreet = "some-street",
            TradingAddressCity = "some-city",
            TradingAddressState = "NY",
            TradingAddressPostalcode = "90210",
            TradingAddressCountry = "US",
            CustomerRisk = "LOW"
        };
    }
}
