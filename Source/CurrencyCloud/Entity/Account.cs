using System;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity
{
    [DataContract]
    public class Account
    {
        [DataMember(Name = "id")]
        public string Id;

        [DataMember(Name = "legal_entity_type")]
        public string LegalEntityType;

        [DataMember(Name = "account_name")]
        public string AccountName;

        [DataMember(Name = "brand")]
        public string Brand;

        [DataMember(Name = "your_reference")]
        public string YourReference;

        [DataMember(Name = "status")]
        public string Status;

        [DataMember(Name = "street")]
        public string Street;

        [DataMember(Name = "city")]
        public string City;

        [DataMember(Name = "state_or_province")]
        public string StateOrProvince;

        [DataMember(Name = "country")]
        public string Country;

        [DataMember(Name = "postal_code")]
        public string PostalCode;

        [DataMember(Name = "spread_table")]
        public string SpreadTable;

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt;

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt;

        [DataMember(Name = "identification_type")]
        public string IdentificationType;

        [DataMember(Name = "identification_value")]
        public string IdentificationValue;

        [DataMember(Name = "short_reference")]
        public string ShortReference;
    }
}
