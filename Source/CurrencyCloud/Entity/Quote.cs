using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class Quote : Entity
    {
        public Quote(string buyCurrency, string sellCurrency, string fixedSide, decimal amount, string holdPeriod)
        {
            this.BuyCurrency = buyCurrency;
            this.SellCurrency = sellCurrency;
            this.FixedSide = fixedSide;
            this.Amount = amount;
            this.HoldPeriod = holdPeriod;
        }

        [JsonConstructor]
        public Quote() { }

        /// <summary>
        /// Three-character ISO code of the currency being purchased
        /// </summary>
        [Param]
        public string BuyCurrency { get; set; }

        /// <summary>
        /// Three-character ISO code of the currency being sold
        /// </summary>
        [Param]
        public string SellCurrency { get; set; }

        /// <summary>
        /// Fix the buy or sell currency
        /// </summary>
        [Param]
        public string FixedSide { get; set; }

        /// <summary>
        /// Amount of the fixed buy or sell currency
        /// </summary>
        [Param]
        public decimal? Amount { get; set; }

        /// <summary>
        /// The length of time for which the quote should remain valid (e.g., "30s", "3m")
        /// </summary>
        [Param]
        public string HoldPeriod { get; set; }

        /// <summary>
        /// Earliest delivery date in UTC time zone. Format YYYY-MM-DD
        /// </summary>
        [Param]
        public DateTime? ConversionDate { get; set; }

        /// <summary>
        /// The preferred strategy to follow to calculate the conversion date
        /// </summary>
        [Param]
        public string ConversionDatePreference { get; set; }

        /// <summary>
        /// The UUID of the quote
        /// </summary>
        public string QuoteId { get; set; }

        public string CurrencyPair { get; set; }

        public decimal? ClientBuyAmount { get; set; }

        public decimal? ClientSellAmount { get; set; }

        public decimal? ClientRate { get; set; }

        public decimal? PartnerBuyAmount { get; set; }

        public decimal? PartnerSellAmount { get; set; }

        public decimal? PartnerRate { get; set; }

        public decimal? CoreRate { get; set; }

        public decimal? MidMarketRate { get; set; }

        public bool? DepositRequired { get; set; }

        public decimal? DepositAmount { get; set; }

        public string DepositCurrency { get; set; }

        public DateTime? ExpiresAt { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? SettlementCutOffTime { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    BuyCurrency,
                    SellCurrency,
                    FixedSide,
                    Amount,
                    HoldPeriod,
                    ConversionDate,
                    ConversionDatePreference,
                    QuoteId,
                    CurrencyPair,
                    ClientBuyAmount,
                    ClientSellAmount,
                    ClientRate,
                    PartnerBuyAmount,
                    PartnerSellAmount,
                    PartnerRate,
                    CoreRate,
                    MidMarketRate,
                    DepositRequired,
                    DepositAmount,
                    DepositCurrency,
                    ExpiresAt,
                    CreatedAt,
                    SettlementCutOffTime
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Quote))
            {
                return false;
            }

            var quote = obj as Quote;

            return BuyCurrency == quote.BuyCurrency &&
                   SellCurrency == quote.SellCurrency &&
                   FixedSide == quote.FixedSide &&
                   Amount == quote.Amount &&
                   HoldPeriod == quote.HoldPeriod &&
                   ConversionDate == quote.ConversionDate &&
                   ConversionDatePreference == quote.ConversionDatePreference &&
                   QuoteId == quote.QuoteId &&
                   CurrencyPair == quote.CurrencyPair &&
                   ClientBuyAmount == quote.ClientBuyAmount &&
                   ClientSellAmount == quote.ClientSellAmount &&
                   ClientRate == quote.ClientRate &&
                   PartnerBuyAmount == quote.PartnerBuyAmount &&
                   PartnerSellAmount == quote.PartnerSellAmount &&
                   PartnerRate == quote.PartnerRate &&
                   CoreRate == quote.CoreRate &&
                   MidMarketRate == quote.MidMarketRate &&
                   DepositRequired == quote.DepositRequired &&
                   DepositAmount == quote.DepositAmount &&
                   DepositCurrency == quote.DepositCurrency &&
                   ExpiresAt == quote.ExpiresAt &&
                   CreatedAt == quote.CreatedAt &&
                   SettlementCutOffTime == quote.SettlementCutOffTime;
        }

        public override int GetHashCode()
        {
            return QuoteId?.GetHashCode() ?? 0;
        }
    }
}
