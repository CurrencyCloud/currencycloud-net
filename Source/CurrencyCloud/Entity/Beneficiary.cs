using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyCloud.Entity
{
    public class Beneficiary : Entity
    {
        
        public Beneficiary(string bankAccountHolderName,
            string bankCountry,
            string currency,
            string name)
        {
            this.BankAccountHolderName = bankAccountHolderName;
            this.BankCountry = bankCountry;
            this.Currency = currency;
            this.Name = name;
        }
        
        [Newtonsoft.Json.JsonConstructor]
        internal Beneficiary() {
        }

        public string Id { get; set; }

        [Param]
        public string BankAccountHolderName { get; set; }

        [Param]
        public string Name { get; set; }

        [Param]
        public string Email { get; set; }

        [Param]
        public List<string> PaymentTypes { get; set; }

        [Param]
        public List<string> BeneficiaryAddress { get; set; }

        [Param]
        public string BeneficiaryCountry { get; set; }

        [Param]
        public string BeneficiaryEntityType { get; set; }

        [Param]
        public string BeneficiaryCompanyName { get; set; }

        [Param]
        public string BeneficiaryFirstName { get; set; }

        [Param]
        public string BeneficiaryLastName { get; set; }

        [Param]
        public string BeneficiaryCity { get; set; }

        [Param]
        public string BeneficiaryPostcode { get; set; }

        [Param]
        public string BeneficiaryStateOrProvince { get; set; }

        [Param]
        public DateTime BeneficiaryDateOfBirth { get; set; }

        [Param]
        public string BeneficiaryIdentificationType { get; set; }

        [Param]
        public string BeneficiaryIdentificationValue { get; set; }

        [Param]
        public string BankCountry { get; set; }

        [Param]
        public string BankName { get; set; }

        [Param]
        public string BankAccountType { get; set; }

        [Param]
        public string Currency { get; set; }

        [Param]
        public string AccountNumber { get; set; }

        [Param]
        public string RoutingCodeType1 { get; set; }

        [Param]
        public string RoutingCodeValue1 { get; set; }

        [Param]
        public string RoutingCodeType2 { get; set; }

        [Param]
        public string RoutingCodeValue2 { get; set; }

        [Param]
        public string BicSwift { get; set; }

        [Param]
        public string Iban { get; set; }

        [Param]
        public bool DefaultBeneficiary { get; set; }

        [Param]
        public string CreatorContactId { get; set; }

        [Param]
        public List<string> BankAddress { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

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
                   UpdatedAt == beneficiary.UpdatedAt;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
