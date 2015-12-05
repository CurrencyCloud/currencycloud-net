using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity
{
    [DataContract]
    public class Beneficiary
    {
        [DataMember(Name = "id")]
        public string Id { get; internal set; }

        [DataMember(Name = "bank_account_holder_name")]
        public string BankAccountHolderName { get; internal set; }

        [DataMember(Name = "name")]
        public string Name { get; internal set; }

        [DataMember(Name = "email")]
        public string Email { get; internal set; }

        [DataMember(Name = "payment_types")]
        public List<string> PaymentTypes { get; internal set; }

        [DataMember(Name = "beneficiary_address")]
        public List<string> BeneficiaryAddress { get; internal set; }

        [DataMember(Name = "beneficiary_country")]
        public string BeneficiaryCountry { get; internal set; }

        [DataMember(Name = "beneficiary_entity_type")]
        public string BeneficiaryEntityType { get; internal set; }

        [DataMember(Name = "beneficiary_company_name")]
        public string BeneficiaryCompanyName { get; internal set; }

        [DataMember(Name = "beneficiary_first_name")]
        public string BeneficiaryFirstName { get; internal set; }

        [DataMember(Name = "beneficiary_last_name")]
        public string BeneficiaryLastName { get; internal set; }

        [DataMember(Name = "beneficiary_city")]
        public string BeneficiaryCity { get; internal set; }

        [DataMember(Name = "beneficiary_postcode")]
        public string BeneficiaryPostcode { get; internal set; }

        [DataMember(Name = "beneficiary_state_or_province")]
        public string BeneficiaryStateOrProvince { get; internal set; }

        [DataMember(Name = "beneficiary_date_of_birth")]
        public DateTime BeneficiaryDateOfBirth { get; internal set; }

        [DataMember(Name = "beneficiary_identification_type")]
        public string BeneficiaryIdentificationType { get; internal set; }

        [DataMember(Name = "beneficiary_identification_value")]
        public string BeneficiaryIdentificationValue { get; internal set; }

        [DataMember(Name = "bank_country")]
        public string BankCountry { get; internal set; }

        [DataMember(Name = "bank_name")]
        public string BankName { get; internal set; }

        [DataMember(Name = "bank_account_type")]
        public string BankAccountType { get; internal set; }

        [DataMember(Name = "currency")]
        public string Currency { get; internal set; }

        [DataMember(Name = "account_number")]
        public string AccountNumber { get; internal set; }

        [DataMember(Name = "routing_code_type_1")]
        public string RoutingCodeType1 { get; internal set; }

        [DataMember(Name = "routing_code_value_1")]
        public string RoutingCodeValue1 { get; internal set; }

        [DataMember(Name = "routing_code_type_2")]
        public string RoutingCodeType2 { get; internal set; }

        [DataMember(Name = "routing_code_value_2")]
        public string RoutingCodeValue2 { get; internal set; }

        [DataMember(Name = "bic_swift")]
        public string BicSwift { get; internal set; }

        [DataMember(Name = "iban")]
        public string Iban { get; internal set; }

        [DataMember(Name = "default_beneficiary")]
        public bool DefaultBeneficiary { get; internal set; }

        [DataMember(Name = "creator_contact_id")]
        public string CreatorContactId { get; internal set; }

        [DataMember(Name = "bank_address")]
        public List<string> BankAddress { get; internal set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; internal set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; internal set; }

        internal Beneficiary() { }
    }
}
