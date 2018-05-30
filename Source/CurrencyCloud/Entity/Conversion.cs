using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class Conversion : Entity
    {
        public Conversion(string buyCurrency, string sellCurrency, string fixedSide, decimal amount, bool termAgreement)
        {
            this.BuyCurrency = buyCurrency;
            this.SellCurrency = sellCurrency;
            this.FixedSide = fixedSide;
            this.Amount = amount;
            this.TermAgreement = termAgreement;
        }

        [JsonConstructor]
        public Conversion() { }

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

        public DateTime? SettlementDate { get; set; }

        ///<summary>
        /// if nothing passed then default uses first_conversion_date
        ///</summary>
        [Param]
        public DateTime? ConversionDate { get; set; }

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

        public decimal? PartnerBuyAmount { get; set; }

        public decimal? PartnerSellAmount { get; set; }

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

        public decimal? MidMarketRate { get; set; }

        public decimal? CoreRate { get; set; }

        public decimal? PartnerRate { get; set; }

        public decimal? ClientRate { get; set; }

        public bool? DepositRequired { get; set; }

        public decimal? DepositAmount { get; set; }

        public string DepositCurrency { get; set; }

        public string DepositStatus { get; set; }

        public DateTime? DepositRequiredAt { get; set; }

        public List<string> PaymentIds { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [Param]
        public string UniqueRequestId { get; set; }

        public decimal? UnallocatedFunds { get; set; }

        [Param]
        public bool? TermAgreement { get; set; }

        [Param]
        public string Reason { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Id,
                    SettlementDate,
                    ConversionDate,
                    ShortReference,
                    CreatorContactId,
                    AccountId,
                    CurrencyPair,
                    Status,
                    BuyCurrency,
                    SellCurrency,
                    ClientBuyAmount,
                    ClientSellAmount,
                    FixedSide,
                    CoreRate,
                    PartnerRate,
                    PartnerStatus,
                    PartnerBuyAmount,
                    PartnerSellAmount,
                    ClientRate,
                    DepositRequired,
                    DepositAmount,
                    DepositCurrency,
                    DepositStatus,
                    DepositRequiredAt,
                    PaymentIds,
                    UnallocatedFunds,
                    UniqueRequestId,
                    CreatedAt,
                    UpdatedAt,
                    MidMarketRate
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

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
                   UpdatedAt == conversion.UpdatedAt &&
                   UniqueRequestId == conversion.UniqueRequestId &&
                   UnallocatedFunds == conversion.UnallocatedFunds &&
                   Reason == conversion.Reason;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
