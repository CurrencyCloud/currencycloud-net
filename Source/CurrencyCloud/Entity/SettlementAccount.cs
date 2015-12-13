using System.Collections.Generic;
using System.Linq;

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

        public override bool Equals(object obj)
        {
            if (!(obj is SettlementAccount))
            {
                return false;
            }

            var settlementAccount = obj as SettlementAccount;

            return BankAccountHolderName == settlementAccount.BankAccountHolderName &&
                   BeneficiaryAddress.SequenceEqual(settlementAccount.BeneficiaryAddress) &&
                   BeneficiaryCountry == settlementAccount.BeneficiaryCountry &&
                   BankName == settlementAccount.BankName &&
                   BankAddress.SequenceEqual(settlementAccount.BankAddress) &&
                   BankCountry == settlementAccount.BankCountry &&
                   Currency == settlementAccount.Currency &&
                   BicSwift == settlementAccount.BicSwift &&
                   Iban == settlementAccount.Iban &&
                   AccountNumber == settlementAccount.AccountNumber &&
                   RoutingCodeType1 == settlementAccount.RoutingCodeType1 &&
                   RoutingCodeValue1 == settlementAccount.RoutingCodeValue1 &&
                   RoutingCodeType2 == settlementAccount.RoutingCodeType2 &&
                   RoutingCodeValue2 == settlementAccount.RoutingCodeValue2;
        }

        public override int GetHashCode()
        {
            return AccountNumber.GetHashCode();
        }
    }
}
