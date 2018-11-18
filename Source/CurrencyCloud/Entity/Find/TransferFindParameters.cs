using System;

namespace CurrencyCloud.Entity
{
    public class TransferFindParameters : FindParameters
    {
        ///<summary>
        /// Short reference code
        ///</summary>
        [Param]
        public string ShortReference { get; set; }

        ///<summary>
        /// Account UUID of the paying account
        ///</summary>
        [Param]
        public string SourceAccountId { get; set; }

        ///<summary>
        /// Account UUID of the receiving account
        ///</summary>
        [Param]
        public string DestinationAccountId { get; set; }

        ///<summary>
        /// Transfer status
        ///</summary>
        [Param]
        public string Status { get; set; }

        ///<summary>
        /// Three-digit currency code
        ///</summary>
        [Param]
        public string Currency { get; set; }

        ///<summary>
        /// Minimum amount
        ///</summary>
        [Param]
        public decimal? AmountFrom { get; set; }

        ///<summary>
        /// Maximum amount
        ///</summary>
        [Param]
        public decimal? AmountTo { get; set; }

        ///<summary>
        /// Any valid ISO 8601 format, eg. "2017-12-31T23:59:59Z"
        ///</summary>
        [Param]
        public DateTime? CreatedAtFrom { get; set; }

        ///<summary>
        /// Any valid ISO 8601 format, eg. "2017-12-31T23:59:59Z"
        ///</summary>
        [Param]
        public DateTime? CreatedAtTo { get; set; }

        ///<summary>
        /// Any valid ISO 8601 format, eg. "2017-12-31T23:59:59Z"
        ///</summary>
        [Param]
        public DateTime? UpdatedAtFrom { get; set; }

        ///<summary>
        /// Any valid ISO 8601 format, eg. "2017-12-31T23:59:59Z"
        ///</summary>
        [Param]
        public DateTime? UpdatedAtTo { get; set; }

        ///<summary>
        /// Any valid ISO 8601 format, eg. "2017-12-31T23:59:59Z"
        ///</summary>
        [Param]
        public DateTime? CompletedAtFrom { get; set; }

        ///<summary>
        /// Any valid ISO 8601 format, eg. "2017-12-31T23:59:59Z"
        ///</summary>
        [Param]
        public DateTime? CompletedAtTo { get; set; }

        ///<summary>
        /// Contact UUID of transfer instructor
        ///</summary>
        [Param]
        public string CreatorAccountId { get; set; }

        ///<summary>
        /// Account UUID of transfer instructor
        ///</summary>
        [Param]
        public string CreatorContactId { get; set; }
    }
}