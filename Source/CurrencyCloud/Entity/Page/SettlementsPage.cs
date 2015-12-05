using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity.Page
{
    [DataContract]
    public class SettlementsPage
    {
        [DataMember(Name = "settlements")]
        public List<Settlement> Settlements { get; internal set; }

        [DataMember(Name = "pagination")]
        public Pagination Pagination { get; internal set; }

        internal SettlementsPage() { }
    }
}
