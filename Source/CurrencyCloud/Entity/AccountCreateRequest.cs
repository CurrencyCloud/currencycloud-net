using System;
using System.Collections.Generic;
using CurrencyCloud.Attributes;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class AccountCreateRequest : Entity
    {
        public AccountCreateRequest(string accountName, string legalEntityType, string street, string city, string postalCode, string country)
        {
            this.AccountName = accountName;
            this.LegalEntityType = legalEntityType;
            this.Street = street;
            this.City = city;
            this.PostalCode = postalCode;
            this.Country = country;
        }

        [JsonConstructor]
        public AccountCreateRequest() { }

        [Param]
        public string LegalEntityType { get; set; }
        [Param]
        public string AccountName { get; set; }
        [Param]
        public string Brand { get; set; }
        [Param]
        public string YourReference { get; set; }
        [Param]
        public string Status { get; set; }
        [Param]
        public string Street { get; set; }
        [Param]
        public string City { get; set; }
        [Param]
        public string StateOrProvince { get; set; }
        [Param]
        public string Country { get; set; }
        [Param]
        public string PostalCode { get; set; }
        [Param]
        public string SpreadTable { get; set; }
        [Param]
        public string IdentificationType { get; set; }
        [Param]
        public string IdentificationValue { get; set; }
        [Param]
        public bool? TermsAndConditionsAccepted { get; set; }
        [Param]
        public bool? ApiTrading { get; set; }
        [Param]
        public bool? OnlineTrading { get; set; }
        [Param]
        public bool? PhoneTrading { get; set; }
        [Param]
        public string LegalEntitySubType { get; set; }
        [Param, DateOnly]
        public DateTime? IdentificationExpiration { get; set; }
        [Param]
        public string IdentificationIssuer { get; set; }
        [Param]
        public string IndustryType { get; set; }
        [Param]
        public string BusinessWebsiteUrl { get; set; }
        [Param]
        public string CountryOfIncorporation { get; set; }
        [Param]
        public string CountryOfCitizenship { get; set; }
        [Param, DateOnly]
        public DateTime? DateOfIncorporation { get; set; }
        [Param]
        public string TradingAddressStreet { get; set; }
        [Param]
        public string TradingAddressCity { get; set; }
        [Param]
        public string TradingAddressState { get; set; }
        [Param]
        public string TradingAddressPostalcode { get; set; }
        [Param]
        public string TradingAddressCountry { get; set; }
        [Param]
        public string TaxIdentification { get; set; }
        [Param]
        public string NationalIdentification { get; set; }
        [Param]
        public string CustomerRisk { get; set; }
        [Param]
        public int? ExpectedMonthlyActivityVolume { get; set; }
        [Param]
        public decimal? ExpectedMonthlyActivityValue { get; set; }
        [Param]
        public List<string> ExpectedTransactionCurrencies { get; set; }
        [Param]
        public List<string> ExpectedTransactionCountries { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    LegalEntityType,
                    AccountName,
                    Brand,
                    YourReference,
                    Status,
                    Street,
                    City,
                    StateOrProvince,
                    Country,
                    PostalCode,
                    SpreadTable,
                    IdentificationType,
                    IdentificationValue,
                    TermsAndConditionsAccepted,
                    ApiTrading,
                    OnlineTrading,
                    PhoneTrading,
                    LegalEntitySubType,
                    IdentificationExpiration,
                    IdentificationIssuer,
                    IndustryType,
                    BusinessWebsiteUrl,
                    CountryOfIncorporation,
                    CountryOfCitizenship,
                    DateOfIncorporation,
                    TradingAddressStreet,
                    TradingAddressCity,
                    TradingAddressState,
                    TradingAddressPostalcode,
                    TradingAddressCountry,
                    TaxIdentification,
                    NationalIdentification,
                    CustomerRisk,
                    ExpectedMonthlyActivityVolume,
                    ExpectedMonthlyActivityValue,
                    ExpectedTransactionCurrencies,
                    ExpectedTransactionCountries,
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

    }
}
