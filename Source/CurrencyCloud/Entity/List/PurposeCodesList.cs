using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.List
{
    public class PurposeCodesList
    {
        internal PurposeCodesList() { }

        public List<PaymentPurposeCode> PurposeCodes { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    PurposeCodes
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}
