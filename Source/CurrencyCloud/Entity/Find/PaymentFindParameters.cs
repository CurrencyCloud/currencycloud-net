using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCloud.Entity
{
    public class PaymentFindParameters : FindParameters
    {

        ///<summary>
        /// Unique human readable identifier
        ///</summary>        
        [Param]
        public string ShortReference { get; set; }

        ///<summary>
        /// 3 character ISO 4217 currency code
        ///</summary>
        [Param]
        public string Currency { get; set; }

        ///<summary>
        /// Amount of Payment to 2dp
        ///</summary>
        [Param]
        public decimal? Amount { get; set; }

        ///<summary>
        /// Amount of Payment to 2dp
        ///</summary>
        [Param]
        public decimal? AmountFrom { get; set; }

        ///<summary>
        /// Amount of Payment to 2dp
        ///</summary>
        [Param]
        public decimal? AmountTo { get; set; }

        ///<summary>
        /// Status of the payment
        ///</summary>
        [Param]
        public string Status { get; set; }

        ///<summary>
        /// Reason for payment
        ///</summary>
        [Param]
        public string Reason { get; set; }

        ///<summary>
        /// ISO 8601 Date when the payment should be paid
        ///</summary>
        [Param]
        public DateTime? PaymentDateFrom { get; set; }

        ///<summary>
        /// ISO 8601 Date when the payment should be paid
        ///</summary>
        [Param]
        public DateTime? PaymentDateTo { get; set; }

        ///<summary>
        /// ISO 8601 Datetime when the payment should be transfered
        ///</summary>
        [Param]
        public DateTime? TransferredAtFrom { get; set; }

        ///<summary>
        /// ISO 8601 Datetime when the payment should be transfered
        ///</summary>
        [Param]
        public DateTime? TransferredAtTo { get; set; }

        ///<summary>
        /// ISO 8601 Datetime when the payment was created
        ///</summary>
        [Param]
        public DateTime? CreatedAtFrom { get; set; }

        ///<summary>
        /// ISO 8601 Datetime when the payment was created
        ///</summary>
        [Param]
        public DateTime? CreatedAtTo { get; set; }

        ///<summary>
        /// ISO 8601 Datetime when the payment was updated
        ///</summary>
        [Param]
        public DateTime? UpdatedAtFrom { get; set; }

        ///<summary>
        /// ISO 8601 Datetime when the payment was updated
        ///</summary>
        [Param]
        public DateTime? UpdatedAtTo { get; set; }

        ///<summary>
        /// ID of the beneficiary
        ///</summary>
        [Param]
        public string BeneficiaryId { get; set; }

        ///<summary>
        /// Conversion unique ID
        ///</summary>
        [Param]
        public string ConversionId { get; set; }

    }
}
