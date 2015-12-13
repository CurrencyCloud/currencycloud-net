using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyCloud.Entity
{
    public class Beneficiary : Entity
    {
        internal Beneficiary() { }

        public string Id { get; set; }

        public string BankAccountHolderName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public List<string> PaymentTypes { get; set; }

        public List<string> BeneficiaryAddress { get; set; }

        public string BeneficiaryCountry { get; set; }

        public string BeneficiaryEntityType { get; set; }

        public string BeneficiaryCompanyName { get; set; }

        public string BeneficiaryFirstName { get; set; }

        public string BeneficiaryLastName { get; set; }

        public string BeneficiaryCity { get; set; }

        public string BeneficiaryPostcode { get; set; }

        public string BeneficiaryStateOrProvince { get; set; }

        public DateTime BeneficiaryDateOfBirth { get; set; }

        public string BeneficiaryIdentificationType { get; set; }

        public string BeneficiaryIdentificationValue { get; set; }

        public string BankCountry { get; set; }

        public string BankName { get; set; }

        public string BankAccountType { get; set; }

        public string Currency { get; set; }

        public string AccountNumber { get; set; }

        public string RoutingCodeType1 { get; set; }

        public string RoutingCodeValue1 { get; set; }

        public string RoutingCodeType2 { get; set; }

        public string RoutingCodeValue2 { get; set; }

        public string BicSwift { get; set; }

        public string Iban { get; set; }

        public bool DefaultBeneficiary { get; set; }

        public string CreatorContactId { get; set; }

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
