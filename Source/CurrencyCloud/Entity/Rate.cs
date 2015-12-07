using System;

namespace CurrencyCloud.Entity
{
    public class Rate : Entity
    {
        internal Rate() { }

        public DateTime SettlementCutOffTime { get; set; }

        public string CurrencyPair { get; set; }

        public string ClientBuyCurrency { get; set; }

        public string ClientSellCurrency { get; set; }

        public decimal ClientBuyAmount { get; set; }

        public decimal ClientSellAmount { get; set; }

        public string FixedSide { get; set; }

        public decimal midMarketRate { get; set; }

        public decimal CoreRate { get; set; }

        public decimal PartnerRate { get; set; }

        public decimal ClientRate { get; set; }

        public bool DepositRequired { get; set; }

        public decimal DepositAmount { get; set; }

        public string DepositCurrency { get; set; }
    }
}
