using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity.Page
{
    [DataContract]
    public class ConversionsPage
    {
        [DataMember(Name = "conversions")]
        public List<Conversion> Conversions { get; internal set; }

        [DataMember(Name = "pagination")]
        public Pagination Pagination { get; internal set; }

        internal ConversionsPage() { }
    }
}
