using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCloud.Entity
{
    public class ConversionFindParameters : FindParameters
    {

        ///<summary>
        /// Unique human readable identifier
        ///</summary>
        [Param]
        public string ShortReference { get; set; }

        ///<summary>
        /// The current status of the Conversion
        ///</summary>
        [Param]
        public string Status { get; set; }

        ///<summary>
        /// The partner status of the Conversion
        ///</summary>
        [Param]
        public string PartnerStatus { get; set; }

        ///<summary>
        /// 3 character ISO 4217 currency code
        ///</summary>
        [Param]
        public string BuyCurrency { get; set; }

        ///<summary>
        /// 3 character ISO 4217 currency code
        ///</summary>
        [Param]
        public string SellCurrency { get; set; }

        ///<summary>
        /// Array of conversion ids
        ///</summary>
        [Param]
        public string[] ConversionIds { get; set; }

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
        /// Currency pair
        ///</summary>
        [Param]
        public string CurrencyPair { get; set; }

        [Param]
        public decimal? PartnerBuyAmountFrom { get; set; }

        [Param]
        public decimal? PartnerBuyAmountTo { get; set; }

        [Param]
        public decimal? PartnerSellAmountFrom { get; set; }

        [Param]
        public decimal? PartnerSellAmountTo { get; set; }

        [Param]
        public decimal? BuyAmountFrom { get; set; }

        [Param]
        public decimal? BuyAmountTo { get; set; }

        [Param]
        public decimal? SellAmountFrom { get; set; }

        [Param]
        public decimal? SellAmountTo { get; set; }
    }
}
