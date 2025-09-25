using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class PaymentValidation : Entity, ResponseAware 
    {
        public PaymentValidation(string validationResult)
        {
            this.ValidationResult = validationResult;
            this.Headers = new Dictionary<string, string>();
        }

        [JsonConstructor]
        public PaymentValidation() 
        {
            this.Headers = new Dictionary<string, string>();
        }

        /// <summary>
        /// Result of the payment validation
        /// </summary>
        public string ValidationResult { get; set; }

        /// <summary>
        /// Custom headers for the validation
        /// </summary>
        [JsonIgnore]
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Gets the value of the 'x-sca-id' header
        /// </summary>
        public string XScaId => Headers.ContainsKey("x-sca-id") ? Headers["x-sca-id"] : null;

        /// <summary>
        /// Gets the value of the 'x-sca-required' header
        /// </summary>
        public bool XScaRequired 
        {
            get
            {
                if (Headers.ContainsKey("x-sca-required"))
                {
                    var value = Headers["x-sca-required"];
                    bool result;
                    bool.TryParse(value, out result);
                    return result;
                }
                return false;
            }
        }

        /// <summary>
        /// Gets the value of the 'x-sca-type' header
        /// </summary>
        public string XScaType => Headers.ContainsKey("x-sca-type") ? Headers["x-sca-type"] : null;

        /// <summary>
        /// Sets a header value
        /// </summary>
        public void SetHeader(string key, string value)
        {
            Headers[key] = value;
        }
    }
}