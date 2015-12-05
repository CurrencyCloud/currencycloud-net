using System;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity
{
    [DataContract]
    public class Currency
    {
        [DataMember(Name = "code")]
        public string Code { get; internal set; }

        [DataMember(Name = "decimal_places")]
        public int DecimalPlaces { get; internal set; }

        [DataMember(Name = "name")]
        public string Name { get; internal set; }

        internal Currency() { }
    }
}
