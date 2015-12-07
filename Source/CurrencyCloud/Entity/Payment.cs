using System;

namespace CurrencyCloud.Entity
{
    public class Payment : Entity
    {
        internal Payment() { }

        public string Id { get; set; }

        public string ShortReference { get; set; }

        public string BeneficiaryId { get; set; }

        public string ConversionId { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Status { get; set; }

        public string PaymentType { get; set; }

        public string Reference { get; set; }

        public string Reason { get; set; }

        public DateTime PaymentDate { get; set; }

        public DateTime TransferredAt { get; set; }

        public int AuthorisationStepsRequired { get; set; }

        public string CreatorContactId { get; set; }

        public string LastUpdaterContactId { get; set; }

        public string FailureReason { get; set; }

        public string PayerId { get; set; }

        public string PayerDetailsSource { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
