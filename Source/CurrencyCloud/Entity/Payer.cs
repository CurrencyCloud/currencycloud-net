using System;
using System.Linq;

namespace CurrencyCloud.Entity
{
    public class Payer : Entity
    {
        [Newtonsoft.Json.JsonConstructor]
        public Payer() { }

        ///<summary>
        /// Unique ID of payer
        ///</summary>
        public string Id { get; set; }

        [Param]
        public string LegalEntityType { get; set; }

        ///<summary>
        /// Payer company name
        ///</summary>
        [Param]
        public string CompanyName { get; set; }

        ///<summary>
        /// Payer first name
        ///</summary>
        [Param]
        public string FirstName { get; set; }

        ///<summary>
        /// Payer second name
        ///</summary>
        [Param]
        public string LastName { get; set; }

        ///<summary>
        /// Payer address
        ///</summary>
        [Param]
        public string Address { get; set; }

        ///<summary>
        /// Payer city
        ///</summary>
        [Param]
        public string City { get; set; }

        ///<summary>
        /// Payer state or province
        ///</summary>
        [Param]
        public string StateOrProvince { get; set; }

        ///<summary>
        /// Payer country
        ///</summary>
        [Param]
        public string Country { get; set; }

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

        ///<summary>
        /// Payer postcode
        ///</summary>
        [Param]
        public string Postcode { get; set; }

        ///<summary>
        /// Payer date of birth(company creation date when payer_entity_type is company)
        ///</summary>
        [Param]
        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

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
