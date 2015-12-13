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

        public override bool Equals(object obj)
        {
            if (!(obj is Transaction))
            {
                return false;
            }

            var transaction = obj as Transaction;

            return Id == transaction.Id &&
                   BalanceId == transaction.BalanceId &&
                   AccountId == transaction.AccountId &&
                   Currency == transaction.Currency &&
                   Amount == transaction.Amount &&
                   BalanceAmount == transaction.BalanceAmount &&
                   Type == transaction.Type &&
                   Action == transaction.Action &&
                   RelatedEntityType == transaction.RelatedEntityType &&
                   RelatedEntityId == transaction.RelatedEntityId &&
                   RelatedEntityShortReference == transaction.RelatedEntityShortReference &&
                   Status == transaction.Status &&
                   Reason == transaction.Reason &&
                   SettlesAt == transaction.SettlesAt &&
                   CreatedAt == transaction.CreatedAt &&
                   UpdatedAt == transaction.UpdatedAt &&
                   CompletedAt == transaction.CompletedAt;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
