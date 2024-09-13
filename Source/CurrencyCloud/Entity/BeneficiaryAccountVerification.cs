// START GENAI@CHATGPT4
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class BeneficiaryAccountVerification
    {
        [JsonConstructor]
        public BeneficiaryAccountVerification() { }

        public BeneficiaryAccountVerification(string answer, string actualName, string reasonCode, string reason)
        {
            this.Answer = answer;
            this.ActualName = actualName;
            this.ReasonCode = reasonCode;
            this.Reason = reason;
        }

        [JsonProperty("answer")]
        public string Answer { get; set; }

        [JsonProperty("actual_name")]
        public string ActualName { get; set; }

        [JsonProperty("reason_code")]
        public string ReasonCode { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}