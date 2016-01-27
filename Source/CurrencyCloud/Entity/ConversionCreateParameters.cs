using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCloud.Entity
{
    public class ConversionCreate
    {
        public ConversionCreate(
            string buyCurrency,
            string sellCurrency,
            string fixedSide,
            decimal amount,
            string reason,
            bool termAgreement
            )
        {
            this.BuyCurrency = buyCurrency;
            this.SellCurrency = sellCurrency;
            this.FixedSide = fixedSide;
            this.Amount = amount;
            this.Reason = reason;
            this.TermAgreement = termAgreement;
        }

        [Param]
        public string BuyCurrency { get; set; }

        [Param]
        public string SellCurrency { get; set; }

        [Param]
        public string FixedSide { get; set; }

        [Param]
        public decimal? Amount { get; set; }

        [Param]
        public string Reason { get; set; }

        [Param]
        public bool TermAgreement { get; set; }

        [Param]
        public DateTime? ConversionDate { get; set; }

        [Param]
        public decimal? ClientBuyAmount { get; set; }

        [Param]
        public decimal? ClientSellAmount { get; set; }

    }
}
