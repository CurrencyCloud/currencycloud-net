using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCloud.Entity
{
    public class BeneficiaryValidateParameters
    {
        public BeneficiaryValidateParameters(string bankCountry,
            string currency,
            string beneficiaryCountry)
        {
            this.BankCountry = bankCountry;
            this.Currency = currency;
            this.BeneficiaryCountry = beneficiaryCountry;
        }

        /// <summary>
        /// Bank account holder's name 
        /// </summary>
        [Param]
        public string BankAccountHolderName { get; set; }

        /// <summary>
        /// Nickname for beneficiary 
        /// </summary>
        [Param]
        public string Name { get; set; }

        /// <summary>
        /// Email address of beneficiary 
        /// </summary>
        [Param]
        public string Email { get; set; }

        ///<summary>
        /// Priority or regular
        ///</summary>
        [Param]
        public List<string> PaymentTypes { get; set; }

        /// <summary>
        /// Address of beneficiary 
        /// </summary>
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
        [Param]
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

        [Param]
        public bool? DefaultBeneficiary { get; set; }

        [Param]
        public string CreatorContactId { get; set; }

        ///<summary>
        /// Address of the bank
        ///</summary>
        [Param]
        public List<string> BankAddress { get; set; }
    }
}
