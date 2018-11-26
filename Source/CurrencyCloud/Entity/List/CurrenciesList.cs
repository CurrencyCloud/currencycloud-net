using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.List
{
    public class CurrenciesList
    {
        internal CurrenciesList() { }

        public struct Currency
        {
            public string Code { get; set; }
            public int DecimalPlaces { get; set; }
            public string Name { get; set; }
            public bool OnlineTrading { get; set; }
            public bool CanBuy { get; set; }
            public bool CanSell { get; set; }
        }

        public List<Currency> Currencies { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Currencies
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}
