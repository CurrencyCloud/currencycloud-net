using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity
{
    [DataContract]
    public class SettlementAccount
    {
        [DataMember(Name = "bank_account_holder_name")]
        public string BankAccountHolderName { get; internal set; }

        [DataMember(Name = "beneficiary_address")]
        public List<string> BeneficiaryAddress { get; internal set; }

        [DataMember(Name = "beneficiary_country")]
        public string BeneficiaryCountry { get; internal set; }

        [DataMember(Name = "bank_name")]
        public string BankName { get; internal set; }

        [DataMember(Name = "bank_address")]
        public List<string> BankAddress { get; internal set; }

        [DataMember(Name = "bank_country")]
        public string BankCountry { get; internal set; }

        [DataMember(Name = "currency")]
        public string Currency { get; internal set; }

        [DataMember(Name = "bic_swift")]
        public string BicSwift { get; internal set; }

        [DataMember(Name = "iban")]
        public string Iban { get; internal set; }

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

        internal SettlementAccount() { }
    }
}
