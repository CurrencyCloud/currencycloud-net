using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity.List
{
    [DataContract]
    public class SettlementAccountsList
    {
        [DataMember(Name = "settlement_accounts")]
        public List<SettlementAccount> SettlementAccounts { get; internal set; }

        internal SettlementAccountsList() { }
    }
}
