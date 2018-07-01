using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class ConversionCancellation : Entity
    {
        [JsonConstructor]
        public ConversionCancellation() { }

        ///<summary>
        /// ID of the conversion
        ///</summary>
        public string ConversionId { get; set; }

        public string AccountId { get; set; }

        public string ContactId { get; set; }

        [Param]
        public string Notes { get; set; }

        ///<summary>
        /// Currency
        ///</summary>
        public string Currency { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? EventDateTime { get; set; }

        public string EventAccountId { get; set; }

        public string EventContactId { get; set; }

        public string EventType { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    ConversionId,
                    ContactId,
                    AccountId,
                    Currency,
                    Amount,
                    Notes,
                    EventDateTime,
                    EventAccountId,
                    EventContactId,
                    EventType
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ConversionCancellation))
            {
                return false;
            }

            var conversionCancellation = obj as ConversionCancellation;

            return ConversionId == conversionCancellation.ConversionId &&
                   AccountId == conversionCancellation.AccountId &&
                   ContactId == conversionCancellation.ContactId &&
                   Notes == conversionCancellation.Notes &&
                   Currency == conversionCancellation.Currency &&
                   Amount == conversionCancellation.Amount &&
                   EventDateTime == conversionCancellation.EventDateTime &&
                   EventAccountId == conversionCancellation.EventAccountId &&
                   EventContactId == conversionCancellation.EventContactId &&
                   EventType == conversionCancellation.EventType;
        }

        public override int GetHashCode()
        {
            return ConversionId.GetHashCode();
        }
    }
}
