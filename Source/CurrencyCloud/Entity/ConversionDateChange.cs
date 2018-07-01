using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class ConversionDateChange : Entity
    {
        [JsonConstructor]
        public ConversionDateChange() { }

        public string ConversionId { get; set; }

        public string Currency { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? NewConversionDate { get; set; }

        [Param]
        public DateTime? NewSettlementDate { get; set; }

        public DateTime? OldConversionDate { get; set; }

        public DateTime? OldSettlementDate { get; set; }

        public DateTime? EventDateTime { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    ConversionId,
                    Amount,
                    Currency,
                    NewConversionDate,
                    NewSettlementDate,
                    OldConversionDate,
                    OldSettlementDate,
                    EventDateTime
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ConversionDateChange))
            {
                return false;
            }

            var conversionDateChange = obj as ConversionDateChange;

            return ConversionId == conversionDateChange.ConversionId &&
                   Currency == conversionDateChange.Currency &&
                   Amount == conversionDateChange.Amount &&
                   NewConversionDate == conversionDateChange.NewConversionDate &&
                   NewSettlementDate == conversionDateChange.NewSettlementDate &&
                   OldConversionDate == conversionDateChange.OldConversionDate &&
                   OldSettlementDate == conversionDateChange.OldSettlementDate &&
                   EventDateTime == conversionDateChange.EventDateTime;
        }

        public override int GetHashCode()
        {
            return ConversionId.GetHashCode();
        }
    }
}
