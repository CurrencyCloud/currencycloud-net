using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.List
{
    public class PaymentChargesSettingsList
    {
        internal PaymentChargesSettingsList() { }

        public struct PaymentChargeSetting
        {
            public string AccountId { get; set; }
            public string ChargeSettingsId { get; set; }
            public bool Enabled { get; set; }
            public bool Default { get; set; }
        }

        public List<PaymentChargeSetting> PaymentChargesSettings { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    PaymentChargesSettings
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

    }
}