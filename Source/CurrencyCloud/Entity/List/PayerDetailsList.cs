using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.List
{
    public class PayerDetailsList
    {
        internal PayerDetailsList() { }

        public struct RequiredField
        {
            public string Name { get; set; }
            public string ValidationRule { get; set; }
        }

        public struct Detail
        {
            public string PayerEntityType { get; set; }
            public string PaymentType { get; set; }
            public List<RequiredField> RequiredFields { get; set; }
            public string PayerIdentificationType { get; set; }
        }

        public List<Detail> Details { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Details
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}
