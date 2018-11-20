using System;

namespace CurrencyCloud.Entity
{
    public class ReportRequestFindParameters : FindParameters
    {
        ///<summary>
        /// Your unique short reference
        ///</summary>
        [Param]
        public string ShortReference { get; set; }

        ///<summary>
        /// Description provided during the report creation process
        ///</summary>
        [Param]
        public string Description { get; set; }

        ///<summary>
        /// ID of account
        ///</summary>
        [Param]
        public string AccountId { get; set; }

        ///<summary>
        /// ID of contact
        ///</summary>
        [Param]
        public string ContactId { get; set; }

        ///<summary>
        /// Start date (for range) when the reports were created
        ///</summary>
        [Param]
        public DateTime? CreatedAtFrom { get; set; }

        ///<summary>
        /// End date (for range) when the reports were created
        ///</summary>
        [Param]
        public DateTime? CreatedAtTo { get; set; }

        ///<summary>
        /// Start date (for range) when the report will be (was) expired
        ///</summary>
        [Param]
        public DateTime? ExpirationDateFrom { get; set; }

        ///<summary>
        /// End date (for range) when the report will be (was) expired
        ///</summary>
        [Param]
        public DateTime? ExpirationDateTo { get; set; }

        ///<summary>
        /// Status of the report
        ///</summary>
        [Param]
        public string Status { get; set; }

        ///<summary>
        /// Type of reports expected to return in search results
        ///</summary>
        [Param]
        public string ReportType { get; set; }
    }
}