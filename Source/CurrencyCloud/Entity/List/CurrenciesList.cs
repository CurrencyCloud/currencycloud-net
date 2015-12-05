using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity.List
{
    [DataContract]
    public class CurrenciesList
    {
        [DataMember(Name = "currencies")]
        public List<Currency> Currencies { get; internal set; }

        internal CurrenciesList() { }
    }
}
