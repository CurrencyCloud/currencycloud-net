using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.List
{
    public class RatesList
    {
        internal RatesList() { }

        public Dictionary<string, List<decimal>> Rates { get; set; }

        public List<string> Unavailable { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Rates,
                    Unavailable
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}
