using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCloud.Entity
{
    public class BeneficiaryFindParameters : FindParameters
    {

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
        /// Priority or regular
        ///</summary>
        [Param]
        public string PaymentTypes { get; set; }

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
        /// Routing code type
        ///</summary>
        [Param]
        public string RoutingCodeType { get; set; }

        ///<summary>
        /// Routing code value
        ///</summary>
        [Param]
        public string RoutingCodeValue { get; set; }

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
        /// Check whether beneficiary is a default
        ///</summary>
        [Param]
        public bool? DefaultBeneficiary { get; set; }

    }
}
