using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCloud.Entity
{
    public class DetailedRateParameters
    {
        public DetailedRateParameters(
            string buyCurrency,
            string sellCurrency,
            string fixedSide,
            decimal amount
            )
        {
            this.BuyCurrency = buyCurrency;
            this.SellCurrency = sellCurrency;
            this.FixedSide = fixedSide;
            this.Amount = amount;
        }

        [Param]
        public string BuyCurrency { get; set; }

        [Param]
        public string SellCurrency { get; set; }

        [Param]
        public string FixedSide { get; set; }

        [Param]
        public decimal Amount { get; set; }

        [Param]
        public DateTime? ConversionDate { get; set; }
    }
}
