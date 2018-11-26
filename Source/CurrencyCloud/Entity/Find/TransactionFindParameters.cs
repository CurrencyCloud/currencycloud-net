using System;

namespace CurrencyCloud.Entity
{
    public class TransactionFindParameters : FindParameters
    {
        ///<summary>
        /// 3 digit ISO 4217 currency code
        ///</summary>
        [Param]
        public string Currency { get; set; }

        ///<summary>
        /// Absolute Amount of the transactions to 2dp
        ///</summary>
        [Param]
        public decimal? Amount { get; set; }

        ///<summary>
        /// Absolute Amount of the transactions to 2dp
        ///</summary>
        [Param]
        public decimal? AmountFrom { get; set; }

        ///<summary>
        /// Absolute Amount of the transactions to 2dp
        ///</summary>
        [Param]
        public decimal? AmountTo { get; set; }

        ///<summary>
        /// The action that caused the transaction to be created - funding, conversion, payment, payment_failure, manual_intervention
        ///</summary>
        [Param]
        public string Action { get; set; }

        /// <summary>
        /// Related Object type that created the transaction - conversion, payment, inbound_funds
        /// </summary>
        [Param]
        public string RelatedEntityType { get; set; }

        ///<summary>
        /// Unique ID of the related Object that created the transaction
        ///</summary>
        [Param]
        public string RelatedEntityId { get; set; }

        ///<summary>
        /// Server generated unique reference for each related object
        ///</summary>
        [Param]
        public string RelatedEntityShortReference { get; set; }

        ///<summary>
        /// Current status of transactions - completed, pending, deleted
        ///</summary>
        [Param]
        public string Status { get; set; }

        ///<summary>
        /// "debit" or "credit"
        ///</summary>
        [Param]
        public string Type { get; set; }

        ///<summary>
        /// Description of transactions - free form text
        ///</summary>
        [Param]
        public string Reason { get; set; }

        ///<summary>
        /// ISO 8601 expected processing date
        ///</summary>
        [Param]
        public DateTime? SettlesAtFrom { get; set; }

        ///<summary>
        /// ISO 8601 expected processing date
        ///</summary>
        [Param]
        public DateTime? SettlesAtTo { get; set; }

        ///<summary>
        /// ISO 8601 Date when the transaction was created
        ///</summary>
        [Param]
        public DateTime? CreatedAtFrom { get; set; }

        ///<summary>
        /// ISO 8601 Date when the transaction was created
        ///</summary>
        [Param]
        public DateTime? CreatedAtTo { get; set; }

        ///<summary>
        /// ISO 8601 Date when the transaction was last updated
        ///</summary>
        [Param]
        public DateTime? UpdatedAtFrom { get; set; }

        ///<summary>
        /// ISO 8601 Date when the transaction was last updated
        ///</summary>
        [Param]
        public DateTime? UpdatedAtTo { get; set; }

        ///<summary>
        /// ISO 8601 Date when the transaction was processed
        ///</summary>
        [Param]
        public DateTime? CompletedAtFrom { get; set; }

        ///<summary>
        /// ISO 8601 Date when the transaction was processed
        ///</summary>
        [Param]
        public DateTime? CompletedAtTo { get; set; }

        ///<summary>
        /// Beneficiary id for payments. Should be passed with related_entity_type = payment
        ///</summary>
        [Param]
        public string BeneficiaryId { get; set; }

        ///<summary>
        /// Conversion currency pair. Should be passed with related_entity_type = conversion
        ///</summary>
        [Param]
        public string CurrencyPair { get; set; }

        ///<summary>
        /// Controls the search of transactions at all account levels. Defaults to own.
        /// own: allows to search transactions on the main account
        /// clients: allows to search transactions of account sub accounts
        /// all: allows to search transactions across account and sub-accounts
        ///</summary>
        [Param]
        public string Scope { get; set; }
    }
}

