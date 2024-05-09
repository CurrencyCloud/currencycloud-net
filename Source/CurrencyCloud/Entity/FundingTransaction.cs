using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class FundingTransaction : Entity
    {
        [JsonConstructor]
        public FundingTransaction() { }

        public string Id { get; set; }

        public decimal? Amount { get; set; }

        public string Currency { get; set; }

        public string Rail { get; set; }

        public string AdditionalInformation { get; set; }

        public string ReceivingAccountRoutingCode { get; set; }

        public DateTime? ValueDate { get; set; }

        public string ReceivingAccountNumber { get; set; }

        public string ReceivingAccountIban { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public FundingTransactionSender Sender { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Id,
                    Amount,
                    Currency,
                    Rail,
                    AdditionalInformation,
                    ReceivingAccountRoutingCode,
                    ValueDate,
                    ReceivingAccountNumber,
                    ReceivingAccountIban,
                    CreatedAt,
                    UpdatedAt,
                    CompletedAt,
                    Sender
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FundingTransaction))
            {
                return false;
            }

            var fundingTransaction = obj as FundingTransaction;

            return Id == fundingTransaction.Id &&
                   Amount == fundingTransaction.Amount &&
                   Currency == fundingTransaction.Currency &&
                   Rail == fundingTransaction.Rail &&
                   AdditionalInformation == fundingTransaction.AdditionalInformation &&
                   ReceivingAccountRoutingCode == fundingTransaction.ReceivingAccountRoutingCode &&
                   ValueDate == fundingTransaction.ValueDate &&
                   ReceivingAccountNumber == fundingTransaction.ReceivingAccountNumber &&
                   ReceivingAccountIban == fundingTransaction.ReceivingAccountIban &&
                   CreatedAt == fundingTransaction.CreatedAt &&
                   UpdatedAt == fundingTransaction.UpdatedAt &&
                   CompletedAt == fundingTransaction.CompletedAt &&
                   Sender == fundingTransaction.Sender;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}