using System.Collections.Generic;

namespace CurrencyCloud.Entity.List
{
    public class RatesList
    {
        internal RatesList() { }

        public Dictionary<string, List<decimal>> Rates { get; set; }

        public List<string> Unavailable { get; set; }
    }
}
