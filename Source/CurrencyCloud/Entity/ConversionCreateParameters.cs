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
            bool termAgreement,
            string uniqueRequestId
            )
        {
            this.BuyCurrency = buyCurrency;
            this.SellCurrency = sellCurrency;
            this.FixedSide = fixedSide;
            this.Amount = amount;
            this.Reason = reason;
            this.TermAgreement = termAgreement;
            this.UniqueRequestId = uniqueRequestId;
        }

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

        [Param]
        public string FixedSide { get; set; }

        [Param]
        public decimal? Amount { get; set; }

        ///<summary>
        /// Reason for currency conversion
        ///</summary>
        [Param]
        public string Reason { get; set; }

        ///<summary>
        /// Indicate if you have agreed to terms and conditions
        ///</summary>
        [Param]
        public bool TermAgreement { get; set; }

        ///<summary>
        /// if nothing passed then default uses first_conversion_date
        ///</summary>
        [Param]
        public DateTime? ConversionDate { get; set; }

        ///<summary>
        /// Set the client buy amount instead of using a spread table
        ///</summary>
        [Param]
        public decimal? ClientBuyAmount { get; set; }

        ///<summary>
        /// Set the client sell amount instead of using a spread table
        ///</summary>
        [Param]
        public decimal? ClientSellAmount { get; set; }
        
        ///<summary>
        /// User provided Idempotency Id
        ///</summary>
        [Param]
        public string UniqueRequestId { get; set; }

    }
}
