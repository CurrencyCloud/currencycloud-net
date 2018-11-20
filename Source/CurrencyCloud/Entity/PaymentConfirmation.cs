using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class PaymentConfirmation : Entity
    {
        [JsonConstructor]
        public PaymentConfirmation() { }

        public string Id { get; set; }

        public string PaymentId { get; set; }

        public string AccountId { get; set; }

        public string ShortReference { get; set; }

        public string Status { get; set; }

        public string ConfirmationUrl { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? ExpiresAt { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Id,
                    PaymentId,
                    AccountId,
                    ShortReference,
                    Status,
                    ConfirmationUrl,
                    CreatedAt,
                    UpdatedAt,
                    ExpiresAt
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PaymentConfirmation))
            {
                return false;
            }

            var paymentConfirmation = obj as PaymentConfirmation;

            return Id == paymentConfirmation.Id &&
                   PaymentId == paymentConfirmation.PaymentId &&
                   AccountId == paymentConfirmation.AccountId &&
                   ShortReference == paymentConfirmation.ShortReference &&
                   Status == paymentConfirmation.Status &&
                   ConfirmationUrl == paymentConfirmation.ConfirmationUrl &&
                   CreatedAt == paymentConfirmation.CreatedAt &&
                   ExpiresAt == paymentConfirmation.ExpiresAt;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}