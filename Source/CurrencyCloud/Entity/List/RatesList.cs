using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity.List
{
    [DataContract]
    public class RatesList
    {
        [DataMember(Name = "rates")]
        public Dictionary<string, List<decimal>> Rates { get; internal set; }

        [DataMember(Name = "unavailable")]
        public List<string> Unavailable { get; internal set; }

        internal RatesList() { }
    }
}
