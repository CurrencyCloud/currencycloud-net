using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class Payment : Entity
    {
        public Payment(string currency, string beneficiaryId, decimal amount, string reason, string reference)
        {
            this.Currency = currency;
            this.BeneficiaryId = beneficiaryId;
            this.Amount = amount;
            this.Reason = reason;
            this.Reference = reference;
        }

        [JsonConstructor]
        public Payment() { }

        ///<summary>
        /// Unique ID of payment
        ///</summary>
        public string Id { get; set; }

        ///<summary>
        /// Unique human readable identifier
        ///</summary>
        public string ShortReference { get; set; }

        ///<summary>
        /// Unique ID of beneficiary
        ///</summary>
        [Param]
        public string BeneficiaryId { get; set; }

        ///<summary>
        /// Conversion unique ID
        ///</summary>
        [Param]
        public string ConversionId { get; set; }

        ///<summary>
        /// Amount of Payment to 2dp
        ///</summary>
        [Param]
        public decimal? Amount { get; set; }

        ///<summary>
        /// 3 character ISO 4217 currency code
        ///</summary>
        [Param]
        public string Currency { get; set; }

        ///<summary>
        /// Status of the payment
        ///</summary>
        public string Status { get; set; }

        ///<summary>
        /// Can be priority or regular
        ///</summary>
        [Param]
        public string PaymentType { get; set; }

        ///<summary>
        /// Reference
        ///</summary>
        [Param]
        public string Reference { get; set; }

        ///<summary>
        /// Reason for payment
        ///</summary>
        [Param]
        public string Reason { get; set; }

        ///<summary>
        /// Purpose code for payment
        ///</summary>
        [Param]
        public string PurposeCode { get; set; }

        ///<summary>
        /// ISO 8601 Date when the payment should be paid
        ///</summary>
        [Param]
        public DateTime? PaymentDate { get; set; }

        public DateTime? TransferredAt { get; set; }

        public int? AuthorisationStepsRequired { get; set; }

        public string CreatorContactId { get; set; }

        public string LastUpdaterContactId { get; set; }

        public string FailureReason { get; set; }

        public string PayerId { get; set; }

        /// <summary>
        /// User-generated idempotency key
        /// </summary>
        [Param]
        public string UniqueRequestId { get; set; }

        ///<summary>
        /// Source for payer details. Can be one of "account" or "payer". If "account" is passed, none of the payer details should be provided.
        ///</summary>
        public string PayerDetailsSource { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string PaymentGroupId { get; set; }

        public decimal? FailureReturnedAmount { get; set; }

        /// <summary>
        /// The name of the ultimate beneficiary if different
        /// </summary>
        [Param]
        public string UltimateBeneficiaryName { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Id,
                    Amount,
                    BeneficiaryId,
                    Currency,
                    Reference,
                    Reason,
                    Status,
                    CreatorContactId,
                    PaymentType,
                    PaymentDate,
                    TransferredAt,
                    AuthorisationStepsRequired,
                    LastUpdaterContactId,
                    ShortReference,
                    ConversionId,
                    FailureReason,
                    PayerId,
                    PayerDetailsSource,
                    CreatedAt,
                    UpdatedAt,
                    PaymentGroupId,
                    UniqueRequestId,
                    FailureReturnedAmount,
                    UltimateBeneficiaryName,
                    PurposeCode
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

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
                   UpdatedAt == payment.UpdatedAt &&
                   PaymentGroupId == payment.PaymentGroupId &&
                   FailureReturnedAmount == payment.FailureReturnedAmount &&
                   UniqueRequestId == payment.UniqueRequestId &&
                   UltimateBeneficiaryName == payment.UltimateBeneficiaryName &&
                   PurposeCode == payment.PurposeCode;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
