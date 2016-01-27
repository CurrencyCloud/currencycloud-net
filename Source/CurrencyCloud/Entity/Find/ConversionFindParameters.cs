using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCloud.Entity
{
    public class ConversionFindParameters : FindParameters
    {

        [Param]
        public string ShortReference { get; set; }

        [Param]
        public string Status { get; set; }

        [Param]
        public string PartnerStatus { get; set; }

        [Param]
        public string BuyCurrency { get; set; }

        [Param]
        public string SellCurrency { get; set; }

        [Param]
        public string[] ConversionIds { get; set; }

        [Param]
        public DateTime? CreatedAtFrom { get; set; }

        [Param]
        public DateTime? CreatedAtTo { get; set; }

        [Param]
        public DateTime? UpdatedAtFrom { get; set; }

        [Param]
        public DateTime? UpdatedAtTo { get; set; }

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
