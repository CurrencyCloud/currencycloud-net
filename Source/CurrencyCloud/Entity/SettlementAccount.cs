using System.Collections.Generic;

namespace CurrencyCloud.Entity
{
    public class SettlementAccount : Entity
    {
        internal SettlementAccount() { }

        public string BankAccountHolderName { get; set; }

        public List<string> BeneficiaryAddress { get; set; }

        public string BeneficiaryCountry { get; set; }

        public string BankName { get; set; }

        public List<string> BankAddress { get; set; }

        public string BankCountry { get; set; }

        public string Currency { get; set; }

        public string BicSwift { get; set; }

        public string Iban { get; set; }

        public string AccountNumber { get; set; }

        public string RoutingCodeType1 { get; set; }

        public string RoutingCodeValue1 { get; set; }

        public string RoutingCodeType2 { get; set; }

        public string RoutingCodeValue2 { get; set; }
    }
}
