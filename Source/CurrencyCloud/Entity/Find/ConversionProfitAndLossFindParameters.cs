using System;

namespace CurrencyCloud.Entity
{
    public class ConversionProfitAndLossFindParameters : FindParameters
    {
        ///<summary>
        /// ID of the Account that the Conversion belongs to
        ///</summary>
        [Param]
        public string AccountId { get; set; }

        ///<summary>
        /// ID of the Contact that did the cancellation
        ///</summary>
        [Param]
        public string ContactId { get; set; }

        ///<summary>
        /// ID of the cancelled conversion
        ///</summary>
        [Param]
        public string ConversionId { get; set; }

        ///<summary>
        /// Event type, in this case 'self_service_cancellation'
        ///</summary>
        [Param]
        public string EventType { get; set; }

        ///<summary>
        /// The currency of the profit and loss. ISO 4217 standard
        ///</summary>
        [Param]
        public string Currency { get; set; }

        ///<summary>
        /// Scope allows contacts to search all profit and losses at all account levels.
        /// Defaults to "own".
        /// own: allows to search profit and losses on the main account
        /// clients: allows to search profit and losses of account
        /// sub accounts all: allows to search balances profit and losses account and subaccounts
        ///</summary>
        [Param]
        public string Scope { get; set; }

        ///<summary>
        /// Allows you to return profit and losses will be done from on or after a defined date/time. ISO 8601 standard
        ///</summary>
        [Param]
        public DateTime? EventDateTimeFrom { get; set; }

        ///<summary>
        /// Allows you to return profit and losses will be done from on or before a defined date/time. ISO 8601 standard
        ///</summary>
        [Param]
        public DateTime? EventDateTimeTo { get; set; }

        ///<summary>
        /// Allows you to return profit and losses based on a minimum profit or loss amount
        ///</summary>
        [Param]
        public decimal? AmountFrom { get; set; }

        ///<summary>
        /// Allows you to return profit and losses based on a maximum profit or loss amount
        ///</summary>
        [Param]
        public decimal? AmountTo { get; set; }
    }
}