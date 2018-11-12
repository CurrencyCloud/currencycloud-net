using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class SenderDetails : Entity
    {
        [JsonConstructor]
        public SenderDetails() { }

        public string Id { get; set; }

        public decimal? Amount { get; set; }

        public string Currency { get; set; }

        public string AdditionalInformation { get; set; }

        public DateTime? ValueDate { get; set; }

        public string Sender { get; set; }

        public string ReceivingAccountNumber { get; set; }

        public string ReceivingAccountIban { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Id,
                    Amount,
                    Currency,
                    AdditionalInformation,
                    ValueDate,
                    Sender,
                    ReceivingAccountNumber,
                    ReceivingAccountIban,
                    CreatedAt,
                    UpdatedAt
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SenderDetails))
            {
                return false;
            }

            var senderDetails = obj as SenderDetails;

            return Id == senderDetails.Id &&
                   Amount == senderDetails.Amount &&
                   Currency == senderDetails.Currency &&
                   AdditionalInformation == senderDetails.AdditionalInformation &&
                   ValueDate == senderDetails.ValueDate &&
                   Sender == senderDetails.Sender &&
                   ReceivingAccountNumber == senderDetails.ReceivingAccountNumber &&
                   ReceivingAccountIban == senderDetails.ReceivingAccountIban &&
                   CreatedAt == senderDetails.CreatedAt &&
                   UpdatedAt == senderDetails.UpdatedAt;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}