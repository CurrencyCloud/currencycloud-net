using System;

namespace CurrencyCloud.Entity
{
    public class SettlementFindParameters : FindParameters
    {

        ///<summary>
        /// Unique human readable identifier
        ///</summary>
        [Param]
        public string ShortReference { get; set; }

        ///<summary>
        /// The current status of the settlement
        ///</summary>
        [Param]
        public string Status { get; set; }

        ///<summary>
        /// ISO 8601 Datetime when the settlement was created
        ///</summary>
        [Param]
        public DateTime? CreatedAtFrom { get; set; }

        ///<summary>
        /// ISO 8601 Datetime when the settlement was created
        ///</summary>
        [Param]
        public DateTime? CreatedAtTo { get; set; }

        ///<summary>
        /// ISO 8601 Datetime when the settlement was updated
        ///</summary>
        [Param]
        public DateTime? UpdatedAtFrom { get; set; }

        ///<summary>
        /// ISO 8601 Datetime when the settlement was updated
        ///</summary>
        [Param]
        public DateTime? UpdatedAtTo { get; set; }

        ///<summary>
        /// ISO 8601 Datetime when the settlement was updated
        ///</summary>
        [Param]
        public DateTime? ReleasedAtFrom { get; set; }

        ///<summary>
        /// ISO 8601 Datetime when the settlement was updated
        ///</summary>
        [Param]
        public DateTime? ReleasedAtTo { get; set; }
    }
}