using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity.List
{
    [DataContract]
    public class BeneficiaryDetailsList
    {
        [DataMember(Name = "details")]
        public Dictionary<string, string> Details { get; internal set; }

        internal BeneficiaryDetailsList() { }
    }
}
