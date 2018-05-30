using System;

namespace CurrencyCloud.Entity
{
    [Obsolete("Class DetailedRateParameters is deprecated. Use a DetailedRates object instead, setting its properties to appropiate values", false)]
    public class DetailedRateParameters
    {
        public DetailedRateParameters(string buyCurrency, string sellCurrency, string fixedSide, decimal amount)
        {
            this.BuyCurrency = buyCurrency;
            this.SellCurrency = sellCurrency;
            this.FixedSide = fixedSide;
            this.Amount = amount;
        }

        ///<summary>
        /// Currency you are converting to
        ///</summary>
        [Param]
        public string BuyCurrency { get; set; }

        ///<summary>
        /// Currency you are converting from
        ///</summary>
        [Param]
        public string SellCurrency { get; set; }

        ///<summary>
        /// The currency that the amount applies to, either buy or sell
        ///</summary>
        [Param]
        public string FixedSide { get; set; }

        ///<summary>
        /// The amount to 2 decimal places
        ///</summary>
        [Param]
        public decimal Amount { get; set; }

        ///<summary>
        /// The date you want the bought currency to be available
        ///</summary>
        [Param]
        public DateTime? ConversionDate { get; set; }
    }
}
