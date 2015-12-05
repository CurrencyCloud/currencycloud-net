using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity.Page
{
    [DataContract]
    public class BalancesPage
    {
        [DataMember(Name = "balances")]
        public List<Balance> Balances { get; internal set; }

        [DataMember(Name = "pagination")]
        public Pagination Pagination { get; internal set; }

        internal BalancesPage() { }
    }
}
