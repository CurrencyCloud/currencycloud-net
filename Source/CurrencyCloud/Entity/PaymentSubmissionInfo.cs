using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class PaymentSubmissionInfo : Entity
    {
        [JsonConstructor]
        public PaymentSubmissionInfo() { }

        public string Status { get; set; }

        public string Message { get; set; }

        public string SubmissionRef { get; set; }

        public string Format { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Status,
                    Message,
                    Format,
                    SubmissionRef
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

    public override bool Equals(object obj)
    {
      if (!(obj is PaymentSubmissionInfo))
      {
        return false;
      }

      var paymentSubmissionInfo = obj as PaymentSubmissionInfo;

      return Status == paymentSubmissionInfo.Status &&
             Message == paymentSubmissionInfo.Message &&
             SubmissionRef == paymentSubmissionInfo.SubmissionRef &&
             Format == paymentSubmissionInfo.Format;
        }

        public override int GetHashCode()
        {
            return Message.GetHashCode();
        }
    }
}