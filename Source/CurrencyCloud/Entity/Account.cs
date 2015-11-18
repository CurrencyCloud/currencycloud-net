using System;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity
{
    [DataContract]
    public class Account
    {
        [DataMember(Name = "id")]
        public string Id { get; internal set; }

        [DataMember(Name = "legal_entity_type")]
        public string LegalEntityType { get; internal set; }

        [DataMember(Name = "account_name")]
        public string AccountName { get; internal set; }

        [DataMember(Name = "brand")]
        public string Brand { get; internal set; }

        [DataMember(Name = "your_reference")]
        public string YourReference { get; internal set; }

        [DataMember(Name = "status")]
        public string Status { get; internal set; }

        [DataMember(Name = "street")]
        public string Street { get; internal set; }

        [DataMember(Name = "city")]
        public string City { get; internal set; }

        [DataMember(Name = "state_or_province")]
        public string StateOrProvince { get; internal set; }

        [DataMember(Name = "country")]
        public string Country { get; internal set; }

        [DataMember(Name = "postal_code")]
        public string PostalCode { get; internal set; }

        [DataMember(Name = "spread_table")]
        public string SpreadTable { get; internal set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; internal set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; internal set; }

        [DataMember(Name = "identification_type")]
        public string IdentificationType { get; internal set; }

        [DataMember(Name = "identification_value")]
        public string IdentificationValue { get; internal set; }

        [DataMember(Name = "short_reference")]
        public string ShortReference { get; internal set; }
    }
}
