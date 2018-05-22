using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class Account : Entity
    {
        public Account(string accountName, string legalEntityType, string street, string city, string postalCode, string country)
        {
            this.AccountName = accountName;
            this.LegalEntityType = legalEntityType;
            this.Street = street;
            this.City = city;
            this.PostalCode = postalCode;
            this.Country = country;
        }

        [JsonConstructor]
        public Account() { }

        /// <summary>
        /// ID of the account
        /// </summary>
        public string Id { get; set; }

        ///<summary>
        /// Account type
        ///</summary>
        [Param]
        public string LegalEntityType { get; set; }

        ///<summary>
        /// Name of the account
        ///</summary>
        [Param]
        public string AccountName { get; set; }

        ///<summary>
        /// The brand to associate with this account. Only available to superbrokers.
        ///</summary>
        [Param]
        public string Brand { get; set; }

        ///<summary>
        /// Your unique customer ID
        ///</summary>
        [Param]
        public string YourReference { get; set; }

        ///<summary>
        /// Status of the account. If nothing is supplied, the default setting is enabled
        ///</summary>
        [Param]
        public string Status { get; set; }

        ///<summary>
        /// First line of the address
        ///</summary>
        [Param]
        public string Street { get; set; }

        ///<summary>
        /// City
        ///</summary>
        [Param]
        public string City { get; set; }

        ///<summary>
        /// State/Province
        ///</summary>
        [Param]
        public string StateOrProvince { get; set; }

        ///<summary>
        /// A two-letter country codes as defined in ISO 3166-1
        ///</summary>
        [Param]
        public string Country { get; set; }

        ///<summary>
        /// Post Code or Zip Code
        ///</summary>
        [Param]
        public string PostalCode { get; set; }

        ///<summary>
        /// Name of spread table
        ///</summary>
        [Param]
        public string SpreadTable { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        ///<summary>
        /// Type of the identification document. One of 'none', 'drivers_license', 'social_security_number', 'green_card', 'passport', 'visa', 'matricula_consular', 'registro_federal_de_contribuyentes', 'credential_de_elector', 'social_insurance_number', 'citizenship_papers', 'drivers_license_canadian', 'existing_credit_card_details', 'employer_identification_number', 'national_id', 'others' or 'incorporation_number'
        ///</summary>
        [Param]
        public string IdentificationType { get; set; }

        ///<summary>
        /// Identification value based on the identification document type. Required if identification_type is set
        ///</summary>
        [Param]
        public string IdentificationValue { get; set; }

        public string ShortReference { get; set; }

        [Param]
        public bool? ApiTrading { get; set; }

        [Param]
        public bool? OnlineTrading { get; set; }

        [Param]
        public bool? PhoneTrading { get; set; }

        public bool? ProcessThirdPartyFunds { get; set; }

        public string SettlementType { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Id,
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
                    LegalEntityType,
                    CreatedAt,
                    UpdatedAt,
                    IdentificationType,
                    IdentificationValue,
                    ShortReference,
                    ApiTrading,
                    OnlineTrading,
                    PhoneTrading,
                    ProcessThirdPartyFunds,
                    SettlementType
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Account))
            {
                return false;
            }

            var account = obj as Account;

            return Id == account.Id &&
                   LegalEntityType == account.LegalEntityType &&
                   AccountName == account.AccountName &&
                   Brand == account.Brand &&
                   YourReference == account.YourReference &&
                   Status == account.Status &&
                   Street == account.Street &&
                   City == account.City &&
                   StateOrProvince == account.StateOrProvince &&
                   Country == account.Country &&
                   PostalCode == account.PostalCode &&
                   SpreadTable == account.SpreadTable &&
                   CreatedAt == account.CreatedAt &&
                   UpdatedAt == account.UpdatedAt &&
                   IdentificationType == account.IdentificationType &&
                   IdentificationValue == account.IdentificationValue &&
                   ShortReference == account.ShortReference &&
                   ApiTrading == account.ApiTrading &&
                   OnlineTrading == account.OnlineTrading &&
                   PhoneTrading == account.PhoneTrading &&
                   ProcessThirdPartyFunds == account.ProcessThirdPartyFunds &&
                   SettlementType == account.SettlementType;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
