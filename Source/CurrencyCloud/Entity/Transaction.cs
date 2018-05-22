using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class Transaction : Entity
    {
        public Transaction() { }

        public string Id { get; set; }

        public string BalanceId { get; set; }

        public string AccountId { get; set; }

        ///<summary>
        /// 3 digit ISO 4217 currency code
        ///</summary>
        public string Currency { get; set; }

        ///<summary>
        /// Absolute Amount of the transactions to 2dp
        ///</summary>
        public decimal? Amount { get; set; }

        public decimal? BalanceAmount { get; set; }

        ///<summary>
        /// "debit" or "credit"
        ///</summary>
        public string Type { get; set; }

        ///<summary>
        /// The action that caused the transaction to be created - funding, conversion, payment, payment_failure, manual_intervention
        ///</summary>
        public string Action { get; set; }

        ///<summary>
        /// Related Object type that created the transaction - conversion, payment, inbound_funds
        ///</summary>
        public string RelatedEntityType { get; set; }

        ///<summary>
        /// Unique ID of the related Object that created the transaction
        ///</summary>
        public string RelatedEntityId { get; set; }

        ///<summary>
        /// Human-readable identifier of the related object
        ///</summary>
        public string RelatedEntityShortReference { get; set; }

        ///<summary>
        /// Current status of transactions - completed, pending, deleted
        ///</summary>
        public string Status { get; set; }

        ///<summary>
        /// Description of transactions - free form text
        ///</summary>
        public string Reason { get; set; }

        public DateTime? SettlesAt { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public string BeneficiaryId { get; set; }

        public string CurrencyPair { get; set; }

        public string Scope { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Id,
                    BalanceId,
                    AccountId,
                    Currency,
                    Amount,
                    BalanceAmount,
                    Type,
                    Action,
                    RelatedEntityType,
                    RelatedEntityId,
                    RelatedEntityShortReference,
                    Status,
                    Reason,
                    SettlesAt,
                    CreatedAt,
                    UpdatedAt,
                    CompletedAt
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

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
                   CompletedAt == transaction.CompletedAt &&
                   BeneficiaryId == transaction.BalanceId &&
                   CurrencyPair == transaction.CurrencyPair &&
                   Scope == transaction.Scope;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
