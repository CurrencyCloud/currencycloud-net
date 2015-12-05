using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity.Page
{
    [DataContract]
    public class BeneficiariesPage
    {
        [DataMember(Name = "accounts")]
        public List<Beneficiary> Beneficiaries { get; internal set; }

        [DataMember(Name = "pagination")]
        public Pagination Pagination { get; internal set; }

        internal BeneficiariesPage() { }
    }
}
