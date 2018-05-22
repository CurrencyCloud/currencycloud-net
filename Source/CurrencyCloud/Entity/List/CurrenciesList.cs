using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.List
{
    public class CurrenciesList
    {
        internal CurrenciesList() { }

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
