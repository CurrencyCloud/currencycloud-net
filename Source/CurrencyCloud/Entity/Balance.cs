using System;

namespace CurrencyCloud.Entity
{
    public class Balance : Entity
    {
        internal Balance() { }

        public string Id { get; set; }

        public string AccountId { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
