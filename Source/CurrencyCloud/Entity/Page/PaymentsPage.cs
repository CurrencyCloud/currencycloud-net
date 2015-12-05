using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity.Page
{
    [DataContract]
    public class PaymentsPage
    {
        [DataMember(Name = "payments")]
        public List<Payment> Payments { get; internal set; }

        [DataMember(Name = "pagination")]
        public Pagination Pagination { get; internal set; }

        internal PaymentsPage() { }
    }
}
