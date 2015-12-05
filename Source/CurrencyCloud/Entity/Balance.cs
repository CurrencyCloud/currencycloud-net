using System;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity
{
    [DataContract]
    public class Balance
    {
        [DataMember(Name = "id")]
        public string Id { get; internal set; }

        [DataMember(Name = "account_id")]
        public string AccountId { get; internal set; }

        [DataMember(Name = "currency")]
        public string Currency { get; internal set; }

        [DataMember(Name = "amount")]
        public decimal Amount { get; internal set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; internal set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; internal set; }

        internal Balance() { }
    }
}
