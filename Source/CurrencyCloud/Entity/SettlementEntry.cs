using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class SettlementEntry : Entity
    {
        internal SettlementEntry() { }

        public decimal SendAmount { get; set; }

        public decimal ReceiveAmount { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    SendAmount,
                    ReceiveAmount
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SettlementEntry))
            {
                return false;
            }

            var settlementEntry = obj as SettlementEntry;

            return SendAmount == settlementEntry.SendAmount &&
                   ReceiveAmount == settlementEntry.ReceiveAmount;
        }

        public override int GetHashCode()
        {
            return (SendAmount + ReceiveAmount).GetHashCode();
        }
    }
}
