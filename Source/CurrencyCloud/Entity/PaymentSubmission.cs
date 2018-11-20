using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class PaymentSubmission : Entity
    {
        [JsonConstructor]
        public PaymentSubmission() { }

        public string Status { get; set; }

        public string Mt103 { get; set; }

        public string SubmissionRef { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Status,
                    Mt103,
                    SubmissionRef
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PaymentSubmission))
            {
                return false;
            }

            var paymentSubmission = obj as PaymentSubmission;

            return Status == paymentSubmission.Status &&
                   Mt103 == paymentSubmission.Mt103 &&
                   SubmissionRef == paymentSubmission.SubmissionRef;
        }

        public override int GetHashCode()
        {
            return Mt103.GetHashCode();
        }
    }
}