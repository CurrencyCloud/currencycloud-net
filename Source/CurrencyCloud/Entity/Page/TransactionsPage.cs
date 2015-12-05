using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity.Page
{
    [DataContract]
    public class TransactionsPage
    {
        [DataMember(Name = "transactions")]
        public List<Transaction> Transactions { get; internal set; }

        [DataMember(Name = "pagination")]
        public Pagination Pagination { get; internal set; }

        internal TransactionsPage() { }
    }
}
