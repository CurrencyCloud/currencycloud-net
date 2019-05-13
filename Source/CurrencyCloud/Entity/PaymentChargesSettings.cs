using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class PaymentChargesSettings : Entity
    {
        public PaymentChargesSettings(string accountId, string chargeSettingsdId)
        {
            this.AccountId = accountId;
            this.ChargeSettingsId = chargeSettingsdId;
        }

        [JsonConstructor]
        PaymentChargesSettings() { }

        [Param]
        public string AccountId { get; set; }

        [Param]
        public string ChargeSettingsId { get; set; }

        [Param]
        public bool? Enabled { get; set; }

        [Param]
        public bool? Default { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    AccountId,
                    ChargeSettingsId,
                    Enabled,
                    Default
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PaymentChargesSettings))
            {
                return false;
            }

            var paymentChargesSettings = obj as PaymentChargesSettings;

            return AccountId == paymentChargesSettings.AccountId &&
                   ChargeSettingsId == paymentChargesSettings.ChargeSettingsId &&
                   Enabled == paymentChargesSettings.Enabled &&
                   Default == paymentChargesSettings.Default;
        }

        public override int GetHashCode()
        {
            return AccountId.GetHashCode();
        }
    }
}