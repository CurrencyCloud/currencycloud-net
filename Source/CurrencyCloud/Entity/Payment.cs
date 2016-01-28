using System;

namespace CurrencyCloud.Entity
{
    public class Payment : Entity
    {
        public Payment(
            string currency,
            string beneficiaryId,
            decimal amount,
            string reason,
            string reference
            )
        {
            this.Currency = currency;
            this.BeneficiaryId = beneficiaryId;
            this.Amount = amount;
            this.Reason = reason;
            this.Reference = reference;
        }

        [Newtonsoft.Json.JsonConstructor]
        internal Payment() { }

        public string Id { get; set; }

        public string ShortReference { get; set; }

        [Param]
        public string BeneficiaryId { get; set; }

        [Param]
        public string ConversionId { get; set; }

        [Param]
        public decimal Amount { get; set; }

        [Param]
        public string Currency { get; set; }

        public string Status { get; set; }

        [Param]
        public string PaymentType { get; set; }

        [Param]
        public string Reference { get; set; }

        [Param]
        public string Reason { get; set; }

        [Param]
        public DateTime? PaymentDate { get; set; }

        public DateTime TransferredAt { get; set; }

        public int AuthorisationStepsRequired { get; set; }

        public string CreatorContactId { get; set; }

        public string LastUpdaterContactId { get; set; }

        public string FailureReason { get; set; }

        public string PayerId { get; set; }

        public string PayerDetailsSource { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Payment))
            {
                return false;
            }

            var payment = obj as Payment;

            return Id == payment.Id &&
                   ShortReference == payment.ShortReference &&
                   BeneficiaryId == payment.BeneficiaryId &&
                   ConversionId == payment.ConversionId &&
                   Amount == payment.Amount &&
                   Currency == payment.Currency &&
                   Status == payment.Status &&
                   PaymentType == payment.PaymentType &&
                   Reference == payment.Reference &&
                   Reason == payment.Reason &&
                   PaymentDate == payment.PaymentDate &&
                   TransferredAt == payment.TransferredAt &&
                   AuthorisationStepsRequired == payment.AuthorisationStepsRequired &&
                   CreatorContactId == payment.CreatorContactId &&
                   LastUpdaterContactId == payment.LastUpdaterContactId &&
                   FailureReason == payment.FailureReason &&
                   PayerId == payment.PayerId &&
                   PayerDetailsSource == payment.PayerDetailsSource &&
                   CreatedAt == payment.CreatedAt &&
                   UpdatedAt == payment.UpdatedAt;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
