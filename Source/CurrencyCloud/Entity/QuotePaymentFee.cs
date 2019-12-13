using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class QuotePaymentFee: Entity
    {
        [JsonConstructor]
        public QuotePaymentFee() { }
        
        public QuotePaymentFee(string AccountId, string PaymentCurrency, 
            string PaymentDestinationCountry, string PaymentType, string ChargeType=null)
        {
            this.AccountId = AccountId;
            this.PaymentCurrency = PaymentCurrency;
            this.PaymentDestinationCountry = PaymentDestinationCountry;
            this.PaymentType = PaymentType;
            this.ChargeType = ChargeType;
        }
            
        /// <summary>
        /// Account identifier type
        /// </summary>
        [Param]
        public string AccountId { get; set; }

        /// <summary>
        /// Payment Currency
        /// </summary>
        [Param]
        public string PaymentCurrency { get; set; }

        /// <summary>
        /// Payment Destination Country
        /// </summary>
        [Param]
        public string PaymentDestinationCountry { get; set; }

        /// <summary>
        /// Payment Type
        /// </summary>
        [Param]
        public string PaymentType { get; set; }

        /// <summary>
        /// Charge Type
        /// </summary>
        [Param]
        public string ChargeType { get; set; }

        /// <summary>
        /// Fee Amount
        /// </summary>
        public decimal FeeAmount { get; set; }

        /// <summary>
        /// Fee Currency
        /// </summary>
        public string FeeCurrency { get; set; }


        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    AccountId,
                    PaymentCurrency,
                    PaymentDestinationCountry,
                    PaymentType,
                    ChargeType,
                    FeeAmount,
                    FeeCurrency
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is QuotePaymentFee))
            {
                return false;
            }

            var quotePaymentFee = obj as QuotePaymentFee;

            return AccountId == quotePaymentFee.AccountId &&
                   PaymentCurrency == quotePaymentFee.PaymentCurrency &&
                   PaymentDestinationCountry == quotePaymentFee.PaymentDestinationCountry &&
                   PaymentType == quotePaymentFee.PaymentType &&
                   ChargeType == quotePaymentFee.ChargeType &&
                   FeeAmount == quotePaymentFee.FeeAmount &&
                   FeeCurrency == quotePaymentFee.FeeCurrency;
        }

        public override int GetHashCode()
        {
            return FeeCurrency.GetHashCode();
        }
        
    }
}