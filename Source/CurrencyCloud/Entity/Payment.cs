using System;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity
{
    [DataContract]
    public class Payment
    {
        [DataMember(Name = "id")]
        public string Id { get; internal set; }

        [DataMember(Name = "short_reference")]
        public string ShortReference { get; internal set; }

        [DataMember(Name = "beneficiary_id")]
        public string BeneficiaryId { get; internal set; }

        [DataMember(Name = "conversion_id")]
        public string ConversionId { get; internal set; }

        [DataMember(Name = "amount")]
        public decimal Amount { get; internal set; }

        [DataMember(Name = "currency")]
        public string Currency { get; internal set; }

        [DataMember(Name = "status")]
        public string Status { get; internal set; }

        [DataMember(Name = "payment_type")]
        public string PaymentType { get; internal set; }

        [DataMember(Name = "reference")]
        public string Reference { get; internal set; }

        [DataMember(Name = "reason")]
        public string Reason { get; internal set; }

        [DataMember(Name = "payment_date")]
        public DateTime PaymentDate { get; internal set; }

        [DataMember(Name = "transferred_at")]
        public DateTime TransferredAt { get; internal set; }

        [DataMember(Name = "authorisation_steps_required")]
        public int AuthorisationStepsRequired { get; internal set; }

        [DataMember(Name = "creator_contact_id")]
        public string CreatorContactId { get; internal set; }

        [DataMember(Name = "last_updater_contact_id")]
        public string LastUpdaterContactId { get; internal set; }

        [DataMember(Name = "failure_reason")]
        public string FailureReason { get; internal set; }

        [DataMember(Name = "payer_id")]
        public string PayerId { get; internal set; }

        [DataMember(Name = "payer_details_source")]
        public string PayerDetailsSource { get; internal set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; internal set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; internal set; }

        internal Payment() { }
    }
}
