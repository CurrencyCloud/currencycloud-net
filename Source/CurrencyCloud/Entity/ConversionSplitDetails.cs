using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class ConversionSplitDetails : Entity
    {
        [JsonConstructor]
        public ConversionSplitDetails() { }

        public string Id { get; set; }

        public string ShortReference { get; set; }

        public decimal? SellAmount { get; set; }

        public string SellCurrency { get; set; }

        public decimal? BuyAmount { get; set; }

        public string BuyCurrency { get; set; }

        public DateTime? SettlementDate { get; set; }

        public DateTime? ConversionDate { get; set; }

        public string Status { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Id,
                    ShortReference,
                    SellAmount,
                    SellCurrency,
                    BuyAmount,
                    BuyCurrency,
                    SettlementDate,
                    ConversionDate,
                    Status
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ConversionSplitDetails))
            {
                return false;
            }

            var conversionSplitDetails = obj as ConversionSplitDetails;

            return Id == conversionSplitDetails.Id &&
                   ShortReference == conversionSplitDetails.ShortReference &&
                   SellAmount == conversionSplitDetails.SellAmount &&
                   SellCurrency == conversionSplitDetails.SellCurrency &&
                   BuyAmount == conversionSplitDetails.BuyAmount &&
                   BuyCurrency == conversionSplitDetails.BuyCurrency &&
                   SettlementDate == conversionSplitDetails.SettlementDate &&
                   ConversionDate == conversionSplitDetails.ConversionDate &&
                   Status == conversionSplitDetails.Status;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}