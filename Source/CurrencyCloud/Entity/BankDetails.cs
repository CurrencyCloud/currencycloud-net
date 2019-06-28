using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class BankDetails : Entity
    {
        [JsonConstructor]
        public BankDetails() { }
            
        /// <summary>
        /// Account identifier type
        /// </summary>
        public string IdentifierType { get; set; }

        /// <summary>
        /// Account identifier value
        /// </summary>
        public string IdentifierValue { get; set; }

        /// <summary>
        /// Account number
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// BIC/SWIFT code
        /// </summary>
        public string BicSwift { get; set; }

        /// <summary>
        /// Name of the bank
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Branch of the bank
        /// </summary>
        public string BankBranch { get; set; }

        /// <summary>
        /// Address of the bank
        /// </summary>
        public string BankAddress { get; set; }

        /// <summary>
        /// City of the bank
        /// </summary>
        public string BankCity { get; set; }

        /// <summary>
        /// State the bank is situated in. Available in all countries using states.
        /// </summary>
        public string BankState { get; set; }

        /// <summary>
        /// The post code of the bank or the zip code for USA banks
        /// </summary>
        public string BankPostCode { get; set; }

        /// <summary>
        /// Full country name
        /// </summary>
        public string BankCountry { get; set; }
        
        /// <summary>
        /// 2 letter ISO country code
        /// </summary>
        ///
        [JsonProperty("bank_country_iso")]
        public string BankCountryISO { get; set; }
        
        /// <summary>
        /// 3 letter ISO currency code
        /// </summary>
        public string Currency { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    IdentifierType,
                    IdentifierValue,
                    AccountNumber,
                    BicSwift,
                    BankName,
                    BankBranch,
                    BankAddress,
                    BankCity,
                    BankState,
                    BankPostCode,
                    BankCountry,
                    BankCountryISO,
                    Currency
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BankDetails))
            {
                return false;
            }

            var bankDetails = obj as BankDetails;

            return IdentifierType == bankDetails.IdentifierType &&
                   IdentifierValue == bankDetails.IdentifierValue &&
                   AccountNumber == bankDetails.AccountNumber &&
                   BicSwift == bankDetails.BicSwift &&
                   BankName == bankDetails.BankName &&
                   BankBranch == bankDetails.BankBranch &&
                   BankAddress == bankDetails.BankAddress &&
                   BankCity == bankDetails.BankCity &&
                   BankState == bankDetails.BankState &&
                   BankPostCode == bankDetails.BankPostCode &&
                   BankCountry == bankDetails.BankCountry &&
                   BankCountryISO == bankDetails.BankCountryISO &&
                   Currency == bankDetails.Currency;
        }

        public override int GetHashCode()
        {
            return IdentifierValue.GetHashCode();
        }
    }
}