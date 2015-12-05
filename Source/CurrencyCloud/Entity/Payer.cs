using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity
{
    [DataContract]
    public class Payer
    {
        [DataMember(Name = "id")]
        public string Id { get; internal set; }

        [DataMember(Name = "legal_entity_type")]
        public string LegalEntityType { get; internal set; }

        [DataMember(Name = "company_name")]
        public string CompanyName { get; internal set; }

        [DataMember(Name = "first_name")]
        public string FirstName { get; internal set; }

        [DataMember(Name = "last_name")]
        public string LastName { get; internal set; }

        [DataMember(Name = "address")]
        public List<string> Address { get; internal set; }

        [DataMember(Name = "city")]
        public string City { get; internal set; }

        [DataMember(Name = "state_or_province")]
        public string StateOrProvince { get; internal set; }

        [DataMember(Name = "identification_type")]
        public string IdentificationType { get; internal set; }

        [DataMember(Name = "identification_value")]
        public string IdentificationValue { get; internal set; }

        [DataMember(Name = "postcode")]
        public string Postcode { get; internal set; }

        [DataMember(Name = "date_of_birth")]
        public DateTime DateOfBirth { get; internal set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; internal set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; internal set; }

        internal Payer() { }
    }
}
