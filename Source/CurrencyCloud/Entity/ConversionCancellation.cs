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

        public string Notes { get; set; }

        ///<summary>
        /// Currency
        ///</summary>
        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public DateTime EventDateTime { get; set; }

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
                    EventDateTime
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}
