using CurrencyCloud.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyCloud.Entity
{
    public class Beneficiary : Entity
    {
        public Beneficiary(string bankAccountHolderName, string bankCountry, string currency, string name)
        {
            this.BankAccountHolderName = bankAccountHolderName;
            this.BankCountry = bankCountry;
            this.Currency = currency;
            this.Name = name;
        }

        [JsonConstructor]
        public Beneficiary() { }

        /// <summary>
        /// ID of the beneficiary
        /// </summary>
        public string Id { get; set; }

        ///<summary>
        /// Bank account holder's name
        ///</summary>
        [Param]
        public string BankAccountHolderName { get; set; }

        ///<summary>
        /// Nickname for beneficiary
        ///</summary>
        [Param]
        public string Name { get; set; }

        ///<summary>
        /// Email address of beneficiary
        ///</summary>
        [Param]
        public string Email { get; set; }

        ///<summary>
        /// Priority or regular
        ///</summary>
        [Param]
        public string[] PaymentTypes { get; set; }

        ///<summary>
        /// Address of beneficiary
        ///</summary>
        [Param]
        public List<string> BeneficiaryAddress { get; set; }

        ///<summary>
        /// A two-letter country code as defined in ISO 3166-1 that defines the beneficiary's country
        ///</summary>
        [Param]
        public string BeneficiaryCountry { get; set; }

        ///<summary>
        /// Type of beneficiary - can be individual or company
        ///</summary>
        [Param]
        public string BeneficiaryEntityType { get; set; }

        ///<summary>
        /// Beneficiary company name
        ///</summary>
        [Param]
        public string BeneficiaryCompanyName { get; set; }

        ///<summary>
        /// Beneficiary first name
        ///</summary>
        [Param]
        public string BeneficiaryFirstName { get; set; }

        ///<summary>
        /// Beneficiary second name
        ///</summary>
        [Param]
        public string BeneficiaryLastName { get; set; }

        ///<summary>
        /// Beneficiary city
        ///</summary>
        [Param]
        public string BeneficiaryCity { get; set; }

        ///<summary>
        /// Beneficiary postcode
        ///</summary>
        [Param]
        public string BeneficiaryPostcode { get; set; }

        ///<summary>
        /// Beneficiary state or province
        ///</summary>
        [Param]
        public string BeneficiaryStateOrProvince { get; set; }

        ///<summary>
        /// Beneficiary date of birth(company creation date when beneficiary_entity_type is company)
        ///</summary>
        [Param, DateOnly]
        public DateTime? BeneficiaryDateOfBirth { get; set; }

        ///<summary>
        /// Type of the identification document. One of 'none', 'drivers_license', 'social_security_number', 'green_card', 'passport', 'visa', 'matricula_consular', 'registro_federal_de_contribuyentes', 'credential_de_elector', 'social_insurance_number', 'citizenship_papers', 'drivers_license_canadian', 'existing_credit_card_details', 'employer_identification_number', 'national_id', 'others' or 'incorporation_number'
        ///</summary>
        [Param]
        public string BeneficiaryIdentificationType { get; set; }

        ///<summary>
        /// Identification value based on the identification document type. Required if identification_type is set
        ///</summary>
        [Param]
        public string BeneficiaryIdentificationValue { get; set; }

        ///<summary>
        /// External reference for the beneficiary
        ///</summary>
        [Param]
        public string BeneficiaryExternalReference { get; set; }

        ///<summary>
        /// A two-letter country code as defined in ISO 3166-1 of the bank account
        ///</summary>
        [Param]
        public string BankCountry { get; set; }

        ///<summary>
        /// Name of the bank
        ///</summary>
        [Param]
        public string BankName { get; set; }

        ///<summary>
        /// Type of bank account
        ///</summary>
        [Param]
        public string BankAccountType { get; set; }

        ///<summary>
        /// 3 character ISO 4217 currency code
        ///</summary>
        [Param]
        public string Currency { get; set; }

        ///<summary>
        /// Account number
        ///</summary>
        [Param]
        public string AccountNumber { get; set; }

        ///<summary>
        /// Routing code type 1
        ///</summary>
        [Param]
        public string RoutingCodeType1 { get; set; }

        ///<summary>
        /// Routing code value 1
        ///</summary>
        [Param]
        public string RoutingCodeValue1 { get; set; }

        ///<summary>
        /// Routing code type 2
        ///</summary>
        [Param]
        public string RoutingCodeType2 { get; set; }

        ///<summary>
        /// Routing code value 2
        ///</summary>
        [Param]
        public string RoutingCodeValue2 { get; set; }

        ///<summary>
        /// 8 or 11 char ISO 9362 BIC/SWIFT code
        ///</summary>
        [Param]
        public string BicSwift { get; set; }

        ///<summary>
        /// up to 34 char international bank account number
        ///</summary>
        [Param]
        public string Iban { get; set; }

        ///<summary>
        /// Set as default beneficiary
        ///</summary>
        [Param]
        public bool? DefaultBeneficiary { get; set; }

        ///<summary>
        /// UUID of creating Contact
        ///</summary>
        [Param]
        public string CreatorContactId { get; set; }

        ///<summary>
        /// Business nature of beneficiary
        ///</summary>
        [Param]
        public string BusinessNature { get; set; }

        ///<summary>
        /// Company Website
        ///</summary>
        [Param]
        public string CompanyWebsite { get; set; }

        ///<summary>
        /// Address of the bank
        ///</summary>
        [Param]
        public List<string> BankAddress { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Id,
                    BankAccountHolderName,
                    Name,
                    Email,
                    PaymentTypes,
                    BeneficiaryAddress,
                    BeneficiaryCountry,
                    BeneficiaryEntityType,
                    BeneficiaryCompanyName,
                    BeneficiaryFirstName,
                    BeneficiaryLastName,
                    BeneficiaryCity,
                    BeneficiaryPostcode,
                    BeneficiaryStateOrProvince,
                    BeneficiaryDateOfBirth,
                    BeneficiaryIdentificationType,
                    BeneficiaryIdentificationValue,
                    BankCountry,
                    BankName,
                    BankAccountType,
                    Currency,
                    AccountNumber,
                    RoutingCodeType1,
                    RoutingCodeValue1,
                    RoutingCodeType2,
                    RoutingCodeValue2,
                    BicSwift,
                    Iban,
                    DefaultBeneficiary,
                    CreatorContactId,
                    BankAddress,
                    CreatedAt,
                    UpdatedAt,
                    BeneficiaryExternalReference,
                    CompanyWebsite,
                    BusinessNature
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Beneficiary))
            {
                return false;
            }

            var beneficiary = obj as Beneficiary;

            return Id == beneficiary.Id &&
                   BankAccountHolderName == beneficiary.BankAccountHolderName &&
                   Name == beneficiary.Name &&
                   Email == beneficiary.Email &&
                   PaymentTypes.SequenceEqual(beneficiary.PaymentTypes) &&
                   BeneficiaryAddress.SequenceEqual(beneficiary.BeneficiaryAddress) &&
                   BeneficiaryCountry == beneficiary.BeneficiaryCountry &&
                   BeneficiaryEntityType == beneficiary.BeneficiaryEntityType &&
                   BeneficiaryCompanyName == beneficiary.BeneficiaryCompanyName &&
                   BeneficiaryFirstName == beneficiary.BeneficiaryFirstName &&
                   BeneficiaryLastName == beneficiary.BeneficiaryLastName &&
                   BeneficiaryCity == beneficiary.BeneficiaryCity &&
                   BeneficiaryPostcode == beneficiary.BeneficiaryPostcode &&
                   BeneficiaryStateOrProvince == beneficiary.BeneficiaryStateOrProvince &&
                   BeneficiaryDateOfBirth == beneficiary.BeneficiaryDateOfBirth &&
                   BeneficiaryIdentificationType == beneficiary.BeneficiaryIdentificationType &&
                   BeneficiaryIdentificationValue == beneficiary.BeneficiaryIdentificationValue &&
                   BankCountry == beneficiary.BankCountry &&
                   BankName == beneficiary.BankName &&
                   BankAccountType == beneficiary.BankAccountType &&
                   Currency == beneficiary.Currency &&
                   AccountNumber == beneficiary.AccountNumber &&
                   RoutingCodeType1 == beneficiary.RoutingCodeType1 &&
                   RoutingCodeValue1 == beneficiary.RoutingCodeValue1 &&
                   RoutingCodeType2 == beneficiary.RoutingCodeType2 &&
                   RoutingCodeValue2 == beneficiary.RoutingCodeValue2 &&
                   BicSwift == beneficiary.BicSwift &&
                   Iban == beneficiary.Iban &&
                   DefaultBeneficiary == beneficiary.DefaultBeneficiary &&
                   CreatorContactId == beneficiary.CreatorContactId &&
                   BankAddress.SequenceEqual(beneficiary.BankAddress) &&
                   CreatedAt == beneficiary.CreatedAt &&
                   UpdatedAt == beneficiary.UpdatedAt &&
                   CompanyWebsite == beneficiary.CompanyWebsite &&
                   BusinessNature == beneficiary.BusinessNature &&
                   BeneficiaryExternalReference == beneficiary.BeneficiaryExternalReference;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
