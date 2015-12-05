using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity.Page
{
    [DataContract]
    public class AccountsPage
    {
        [DataMember(Name = "accounts")]
        public List<Account> Accounts { get; internal set; }

        [DataMember(Name = "pagination")]
        public Pagination Pagination { get; internal set; }

        internal AccountsPage() { }
    }
}
