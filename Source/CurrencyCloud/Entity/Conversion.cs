using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity
{
    [DataContract]
    public class Conversion
    {
        [DataMember(Name = "id")]
        public string Id { get; internal set; }

        [DataMember(Name = "account_id")]
        public string AccountId { get; internal set; }

        [DataMember(Name = "creator_contact_id")]
        public string CreatorContactId { get; internal set; }

        [DataMember(Name = "short_reference")]
        public string ShortReference { get; internal set; }

        [DataMember(Name = "settlement_date")]
        public DateTime SettlementDate { get; internal set; }

        [DataMember(Name = "conversion_date")]
        public DateTime ConversionDate { get; internal set; }

        [DataMember(Name = "status")]
        public string Status { get; internal set; }

        [DataMember(Name = "partner_status")]
        public string PartnerStatus { get; internal set; }

        [DataMember(Name = "currency_pair")]
        public string CurrencyPair { get; internal set; }

        [DataMember(Name = "buy_currency")]
        public string BuyCurrency { get; internal set; }

        [DataMember(Name = "sell_currency")]
        public string SellCurrency { get; internal set; }

        [DataMember(Name = "fixed_side")]
        public string FixedSide { get; internal set; }

        [DataMember(Name = "partner_buy_amount")]
        public decimal PartnerBuyAmount { get; internal set; }

        [DataMember(Name = "partner_sell_amount")]
        public decimal PartnerSellAmount { get; internal set; }

        [DataMember(Name = "client_buy_amount")]
        public decimal ClientBuyAmount { get; internal set; }

        [DataMember(Name = "client_sell_amount")]
        public decimal clientSellAmount { get; internal set; }

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

        [DataMember(Name = "deposit_status")]
        public string DepositStatus { get; internal set; }

        [DataMember(Name = "deposit_required_at")]
        public DateTime DepositRequiredAt { get; internal set; }

        [DataMember(Name = "payment_ids")]
        public List<string> PaymentIds { get; internal set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; internal set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; internal set; }

        internal Conversion() { }
    }
}
