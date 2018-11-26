using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.List
{
    public class PaymentPurposeCodeList
    {
        internal PaymentPurposeCodeList() { }

        public struct PaymentPurposeCode
        {
            public string Currency { get; set; }
            public string EntityType { get; set; }
            public string PurposeCode { get; set; }
            public string PurposeDescription { get; set; }
            public string BankAccountCountry { get; set; }
        }

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
