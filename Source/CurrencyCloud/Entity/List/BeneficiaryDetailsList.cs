using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.List
{
    public class BeneficiaryDetailsList
    {
        internal BeneficiaryDetailsList() { }

        public struct Detail
        {
            public string PaymentType { get; set; }
            public string AcctNumber { get; set; }
            public string BicSwift { get; set; }
            public string BeneficiaryEntityType { get; set; }
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
