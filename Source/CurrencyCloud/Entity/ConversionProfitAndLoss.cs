using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class ConversionProfitAndLoss : Entity
    {
        [JsonConstructor]
        public ConversionProfitAndLoss() { }

        /// <summary>
        /// ID of the Account that the Conversion belongs to
        /// </summary>
        [Param]
        public string AccountId { get; set; }

        /// <summary>
        /// ID of the Contact that did the cancellation
        /// </summary>
        [Param]
        public string ContactId { get; set; }

        public string EventAccountId { get; set; }

        public string EventContactId { get; set; }

        /// <summary>
        /// ID of the cancelled conversion
        /// </summary>
        [Param]
        public string ConversionId { get; set; }

        /// <summary>
        /// Event type, in this case 'self_service_cancellation'
        /// </summary>
        [Param]
        public string EventType { get; set; }

        public decimal? Amount { get; set; }

        /// <summary>
        /// The currency of the profit and loss. ISO 4217 standard
        /// </summary>
        [Param]
        public string Currency { get; set; }

        public string Notes { get; set; }

        public DateTime? EventDateTime { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    AccountId,
                    ContactId,
                    EventAccountId,
                    EventContactId,
                    ConversionId,
                    EventType,
                    Amount,
                    Currency,
                    Notes,
                    EventDateTime
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ConversionProfitAndLoss))
            {
                return false;
            }

            var conversionProfitAndLoss = obj as ConversionProfitAndLoss;

            return AccountId == conversionProfitAndLoss.AccountId &&
                   ContactId == conversionProfitAndLoss.ContactId &&
                   EventAccountId == conversionProfitAndLoss.EventAccountId &&
                   EventContactId == conversionProfitAndLoss.EventContactId &&
                   ConversionId == conversionProfitAndLoss.ConversionId &&
                   EventType == conversionProfitAndLoss.EventType &&
                   Amount == conversionProfitAndLoss.Amount &&
                   Currency == conversionProfitAndLoss.Currency &&
                   Notes == conversionProfitAndLoss.Notes &&
                   EventDateTime == conversionProfitAndLoss.EventDateTime;
        }

        public override int GetHashCode()
        {
            return ConversionId.GetHashCode();
        }
    }
}