using System.Runtime.Serialization;

namespace CurrencyCloud.Entity
{
    [DataContract]
    public class SettlementEntry
    {
        [DataMember(Name = "send_amount")]
        public decimal SendAmount { get; internal set; }

        [DataMember(Name = "receive_amount")]
        public decimal ReceiveAmount { get; internal set; }

        internal SettlementEntry() { }
    }
}
