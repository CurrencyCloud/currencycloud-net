using System;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity
{
    [DataContract]
    class Rate
    {
        [DataMember(Name = "settlement_cut_off_time")]
        public DateTime SettlementCutOffTime { get; internal set; }

        [DataMember(Name = "currency_pair")]
        public string CurrencyPair { get; internal set; }

        [DataMember(Name = "client_buy_currency")]
        public string ClientBuyCurrency { get; internal set; }

        [DataMember(Name = "client_sell_currency")]
        public string ClientSellCurrency { get; internal set; }

        [DataMember(Name = "client_buy_amount")]
        public decimal ClientBuyAmount { get; internal set; }

        [DataMember(Name = "client_sell_amount")]
        public decimal ClientSellAmount { get; internal set; }

        [DataMember(Name = "fixed_side")]
        public string FixedSide { get; internal set; }

        [DataMember(Name = "mid_market_rate")]
        public decimal midMarketRate { get; internal set; }

        [DataMember(Name = "core_rate")]
        public decimal CoreRate { get; internal set; }

        [DataMember(Name = "partner_rate")]
        public decimal PartnerRate { get; internal set; }

        [DataMember(Name = "client_rate")]
        public decimal ClientRate { get; internal set; }

        [DataMember(Name = "deposit_required")]
        public bool DepositRequired { get; internal set; }

        [DataMember(Name = "deposit_amount")]
        public decimal DepositAmount { get; internal set; }

        [DataMember(Name = "deposit_currency")]
        public string DepositCurrency { get; internal set; }

        internal Rate() { }
    }
}
