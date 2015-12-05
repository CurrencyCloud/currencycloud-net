using System;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity
{
    [DataContract]
    public class Transaction
    {
        [DataMember(Name = "id")]
        public string Id { get; internal set; }

        [DataMember(Name = "balance_id")]
        public string BalanceId { get; internal set; }

        [DataMember(Name = "account_id")]
        public string AccountId { get; internal set; }

        [DataMember(Name = "currency")]
        public string Currency { get; internal set; }

        [DataMember(Name = "amount")]
        public decimal Amount { get; internal set; }

        [DataMember(Name = "balance_amount")]
        public decimal BalanceAmount { get; internal set; }

        [DataMember(Name = "type")]
        public string Type { get; internal set; }

        [DataMember(Name = "action")]
        public string Action { get; internal set; }

        [DataMember(Name = "related_entity_type")]
        public string RelatedEntityType { get; internal set; }

        [DataMember(Name = "related_entity_id")]
        public string RelatedEntityId { get; internal set; }

        [DataMember(Name = "related_entity_short_reference")]
        public string RelatedEntityShortReference { get; internal set; }

        [DataMember(Name = "status")]
        public string Status { get; internal set; }

        [DataMember(Name = "reason")]
        public string Reason { get; internal set; }

        [DataMember(Name = "settles_at")]
        public DateTime SettlesAt { get; internal set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; internal set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; internal set; }

        [DataMember(Name = "completed_at")]
        public DateTime CompletedAt{ get; internal set; }

        internal Transaction() { }
    }
}
