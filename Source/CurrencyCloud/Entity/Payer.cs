using System;
using System.Linq;
using CurrencyCloud.Attributes;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class Payer : Entity
    {
        [JsonConstructor]
        public Payer() { }

        ///<summary>
        /// Unique ID of payer
        ///</summary>
        public string Id { get; set; }

        public string LegalEntityType { get; set; }

        ///<summary>
        /// Payer company name
        ///</summary>
        public string CompanyName { get; set; }

        ///<summary>
        /// Payer forename
        ///</summary>
        public string FirstName { get; set; }

        ///<summary>
        /// Payer surname
        ///</summary>
        public string LastName { get; set; }

        ///<summary>
        /// Payer address
        ///</summary>
        public string Address { get; set; }

        ///<summary>
        /// Payer city
        ///</summary>
        public string City { get; set; }

        ///<summary>
        /// Payer state or province
        ///</summary>
        public string StateOrProvince { get; set; }

        ///<summary>
        /// Payer country
        ///</summary>
        public string Country { get; set; }

        ///<summary>
        /// Type of the identification document. One of 'none', 'drivers_license', 'social_security_number', 'green_card', 'passport', 'visa', 'matricula_consular', 'registro_federal_de_contribuyentes', 'credential_de_elector', 'social_insurance_number', 'citizenship_papers', 'drivers_license_canadian', 'existing_credit_card_details', 'employer_identification_number', 'national_id', 'others' or 'incorporation_number'
        ///</summary>
        public string IdentificationType { get; set; }

        ///<summary>
        /// Identification value based on the identification document type. Required if identification_type is set
        ///</summary>
        public string IdentificationValue { get; set; }

        ///<summary>
        /// Payer postcode
        ///</summary>
        public string Postcode { get; set; }

        ///<summary>
        /// Payer date of birth(company creation date when payer_entity_type is company)
        ///</summary>
        public DateTime? DateOfBirth { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Id,
                    LegalEntityType,
                    CompanyName,
                    FirstName,
                    LastName,
                    Address,
                    City,
                    StateOrProvince,
                    Country,
                    IdentificationType,
                    IdentificationValue,
                    Postcode,
                    DateOfBirth,
                    CreatedAt,
                    UpdatedAt
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Payer))
            {
                return false;
            }

            var payer = obj as Payer;

            return Id == payer.Id &&
                   LegalEntityType == payer.LegalEntityType &&
                   CompanyName == payer.CompanyName &&
                   FirstName == payer.FirstName &&
                   LastName == payer.LastName &&
                   Address.SequenceEqual(payer.Address) &&
                   City == payer.City &&
                   StateOrProvince == payer.StateOrProvince &&
                   IdentificationType == payer.IdentificationType &&
                   IdentificationValue == payer.IdentificationValue &&
                   Postcode == payer.Postcode &&
                   DateOfBirth == payer.DateOfBirth &&
                   CreatedAt == payer.CreatedAt &&
                   UpdatedAt == payer.UpdatedAt;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
