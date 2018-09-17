using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class Transfer
    {
        public Transfer(string sourceAccountId, string destinationAccountId, string currency, decimal amount)
        {
            this.SourceAccountId = sourceAccountId;
            this.DestinationAccountId = destinationAccountId;
            this.Currency = currency;
            this.Amount = amount;
        }

        [JsonConstructor]
        public Transfer() { }

        public string Id { get; set; }

        public string ShortReference { get; set; }

        [Param]
        public string SourceAccountId { get; set; }

        [Param]
        public string DestinationAccountId { get; set; }

        [Param]
        public string Currency { get; set; }

        [Param]
        public decimal? Amount { get; set; }

        [Param]
        public string Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public string CreatorAccountId { get; set; }

        public string CreatorContactId { get; set; }

        [Param]
        public string Reason { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Id,
                    ShortReference,
                    SourceAccountId,
                    DestinationAccountId,
                    Currency,
                    Amount,
                    Status,
                    Reason,
                    CreatedAt,
                    UpdatedAt,
                    CompletedAt,
                    CreatorAccountId,
                    CreatorContactId
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Transfer))
            {
                return false;
            }

            var transfer = obj as Transfer;

            return Id == transfer.Id &&
                   ShortReference == transfer.ShortReference &&
                   SourceAccountId == transfer.SourceAccountId &&
                   DestinationAccountId == transfer.DestinationAccountId &&
                   Currency == transfer.Currency &&
                   Amount == transfer.Amount &&
                   Status == transfer.Status &&
                   CreatedAt == transfer.CreatedAt &&
                   UpdatedAt == transfer.UpdatedAt &&
                   CompletedAt == transfer.CompletedAt &&
                   CreatorAccountId == transfer.CreatorAccountId &&
                   CreatorContactId == transfer.CreatorContactId &&
                   Reason == transfer.Reason;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}