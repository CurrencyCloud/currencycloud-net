using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyCloud.Entity
{
    public class Conversion : Entity
    {

        [Newtonsoft.Json.JsonConstructor]
        internal Conversion() { }

        ///<summary>
        /// ID of the conversion
        ///</summary>
        public string Id { get; set; }

        public string AccountId { get; set; }

        public string CreatorContactId { get; set; }

        ///<summary>
        /// Unique human readable identifier
        ///</summary>
        public string ShortReference { get; set; }

        public DateTime SettlementDate { get; set; }

        ///<summary>
        /// if nothing passed then default uses first_conversion_date
        ///</summary>
        public DateTime ConversionDate { get; set; }

        ///<summary>
        /// The current status of the Conversion
        ///</summary>
        public string Status { get; set; }

        ///<summary>
        /// The partner status of the Conversion
        ///</summary>
        public string PartnerStatus { get; set; }

        ///<summary>
        /// Currency pair
        ///</summary>
        public string CurrencyPair { get; set; }

        ///<summary>
        /// 3 character ISO 4217 currency code
        ///</summary>
        public string BuyCurrency { get; set; }

        ///<summary>
        /// 3 character ISO 4217 currency code
        ///</summary>
        public string SellCurrency { get; set; }

        public string FixedSide { get; set; }

        public decimal PartnerBuyAmount { get; set; }

        public decimal PartnerSellAmount { get; set; }

        ///<summary>
        /// Set the client buy amount instead of using a spread table
        ///</summary>
        public decimal ClientBuyAmount { get; set; }

        ///<summary>
        /// Set the client sell amount instead of using a spread table
        ///</summary>
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
