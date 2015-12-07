using System;

namespace CurrencyCloud.Entity
{
    public class Transaction : Entity
    {
        internal Transaction() { }

        public string Id { get; set; }

        public string BalanceId { get; set; }

        public string AccountId { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public decimal BalanceAmount { get; set; }

        public string Type { get; set; }

        public string Action { get; set; }

        public string RelatedEntityType { get; set; }

        public string RelatedEntityId { get; set; }

        public string RelatedEntityShortReference { get; set; }

        public string Status { get; set; }

        public string Reason { get; set; }

        public DateTime SettlesAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime CompletedAt{ get; set; }
    }
}
