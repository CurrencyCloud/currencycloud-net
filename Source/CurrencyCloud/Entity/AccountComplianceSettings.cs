using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class AccountComplianceSettings : Entity
    {
        public AccountComplianceSettings(string accountId)
        {
            this.AccountId = accountId;
        }

        [JsonConstructor]
        AccountComplianceSettings() { }

        [Param]
        public string AccountId { get; set; }

        [Param]
        public string IndustryType { get; set; }

        [Param]
        public string CountryOfIncorporation { get; set; }

        [Param]
        public DateOnly? DateOfIncorporation { get; set; }

        [Param]
        public string BusinessWebsiteUrl { get; set; }

        [Param]
        public List<string> ExpectedTransactionCountries { get; set; }

        [Param]
        public List<string>  ExpectedTransactionCurrencies { get; set; }

        [Param]
        public int? ExpectedMonthlyActivityVolume { get; set; }

        [Param]
        public decimal? ExpectedMonthlyActivityValue { get; set; }

        [Param]
        public string TaxIdentification { get; set; }

        [Param]
        public string NationalIdentification { get; set; }

        [Param]
        public string CountryOfCitizenship { get; set; }

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
        public string CustomerRisk { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    IndustryType,
                    CountryOfIncorporation,
                    DateOfIncorporation,
                    BusinessWebsiteUrl,
                    ExpectedTransactionCountries,
                    ExpectedTransactionCurrencies,
                    ExpectedMonthlyActivityVolume,
                    ExpectedMonthlyActivityValue,
                    TaxIdentification,
                    NationalIdentification,
                    CountryOfCitizenship,
                    TradingAddressStreet,
                    TradingAddressCity,
                    TradingAddressState,
                    TradingAddressPostalcode,
                    TradingAddressCountry,
                    CustomerRisk
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is AccountComplianceSettings))
            {
                return false;
            }

            var other = obj as AccountComplianceSettings;

            return AccountId == other.AccountId &&
                IndustryType == other.IndustryType &&
                CountryOfIncorporation == other.CountryOfIncorporation &&
                DateOfIncorporation == other.DateOfIncorporation &&
                BusinessWebsiteUrl == other.BusinessWebsiteUrl &&
                ExpectedTransactionCountries == other.ExpectedTransactionCountries &&
                ExpectedTransactionCurrencies == other.ExpectedTransactionCurrencies &&
                ExpectedMonthlyActivityVolume == other.ExpectedMonthlyActivityVolume &&
                ExpectedMonthlyActivityValue == other.ExpectedMonthlyActivityValue &&
                TaxIdentification == other.TaxIdentification &&
                NationalIdentification == other.NationalIdentification &&
                CountryOfCitizenship == other.CountryOfCitizenship &&
                TradingAddressStreet == other.TradingAddressStreet &&
                TradingAddressCity == other.TradingAddressCity &&
                TradingAddressState == other.TradingAddressState &&
                TradingAddressPostalcode == other.TradingAddressPostalcode &&
                TradingAddressCountry == other.TradingAddressCountry &&
                CustomerRisk == other.CustomerRisk;
        }

        public override int GetHashCode()
        {
            return AccountId.GetHashCode();
        }
    }
}
