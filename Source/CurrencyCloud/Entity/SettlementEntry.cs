namespace CurrencyCloud.Entity
{
    public class SettlementEntry : Entity
    {
        internal SettlementEntry() { }

        public decimal SendAmount { get; set; }

        public decimal ReceiveAmount { get; set; }
    }
}
