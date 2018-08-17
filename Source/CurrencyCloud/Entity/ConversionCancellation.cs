using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class ConversionCancellation : Entity
    {
        [JsonConstructor]
        public ConversionCancellation() { }

        public ConversionCancellation(string id, string notes = null)
        {
            this.ContactId = id;
            this.Notes = notes;
        }

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
                   ContactId == conversionCancellation.ContactId &&
                   AccountId == conversionCancellation.AccountId &&
                   Currency == conversionCancellation.Currency &&
                   Amount == conversionCancellation.Amount &&
                   Notes == conversionCancellation.Notes &&
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
