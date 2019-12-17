using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.List
{
    public class PaymentFeeRulesList
    {
        internal PaymentFeeRulesList() { }
        
        public struct PaymentFeeRule
        {
            public string PaymentType { get; set; }
            public string ChargeType { get; set; }
            public decimal FeeAmount { get; set; }
            public string FeeCurrency { get; set; }
        }
        
        public List<PaymentFeeRule> PaymentFeeRules { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    PaymentFeeRules
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}