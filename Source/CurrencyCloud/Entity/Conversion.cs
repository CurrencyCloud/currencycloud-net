using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyCloud.Entity
{
    public class Conversion : Entity
    {
        internal Conversion() { }

        public string Id { get; set; }

        public string AccountId { get; set; }

        public string CreatorContactId { get; set; }

        public string ShortReference { get; set; }

        public DateTime SettlementDate { get; set; }

        public DateTime ConversionDate { get; set; }

        public string Status { get; set; }

        public string PartnerStatus { get; set; }

        public string CurrencyPair { get; set; }

        public string BuyCurrency { get; set; }

        public string SellCurrency { get; set; }

        public string FixedSide { get; set; }

        public decimal PartnerBuyAmount { get; set; }

        public decimal PartnerSellAmount { get; set; }

        public decimal ClientBuyAmount { get; set; }

        public decimal ClientSellAmount { get; set; }

        public decimal MidMarketRate { get; set; }

        public decimal CoreRate { get; set; }

        public decimal PartnerRate { get; set; }

        public decimal ClientRate { get; set; }

        public bool DepositRequired { get; set; }

        public decimal DepositAmount { get; set; }

        public string DepositCurrency { get; set; }

        public string DepositStatus { get; set; }

        public DateTime DepositRequiredAt { get; set; }

        public List<string> PaymentIds { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Conversion))
            {
                return false;
            }

            var conversion = obj as Conversion;

            return Id == conversion.Id &&
                   AccountId == conversion.AccountId &&
                   CreatorContactId == conversion.CreatorContactId &&
                   ShortReference == conversion.ShortReference &&
                   SettlementDate == conversion.SettlementDate &&
                   ConversionDate == conversion.ConversionDate &&
                   Status == conversion.Status &&
                   PartnerStatus == conversion.PartnerStatus &&
                   CurrencyPair == conversion.CurrencyPair &&
                   BuyCurrency == conversion.BuyCurrency &&
                   AccountId == conversion.AccountId &&
                   SellCurrency == conversion.SellCurrency &&
                   FixedSide == conversion.FixedSide &&
                   PartnerBuyAmount == conversion.PartnerBuyAmount &&
                   PartnerSellAmount == conversion.PartnerSellAmount &&
                   ClientBuyAmount == conversion.ClientBuyAmount &&
                   ClientSellAmount == conversion.ClientSellAmount &&
                   MidMarketRate == conversion.MidMarketRate &&
                   CoreRate == conversion.CoreRate &&
                   PartnerRate == conversion.PartnerRate &&
                   ClientRate == conversion.ClientRate &&
                   DepositRequired == conversion.DepositRequired &&
                   DepositAmount == conversion.DepositAmount &&
                   DepositCurrency == conversion.DepositCurrency &&
                   DepositStatus == conversion.DepositStatus &&
                   DepositRequiredAt == conversion.DepositRequiredAt &&
                   PaymentIds.SequenceEqual(conversion.PaymentIds) &&
                   CreatedAt == conversion.CreatedAt &&
                   UpdatedAt == conversion.UpdatedAt;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
