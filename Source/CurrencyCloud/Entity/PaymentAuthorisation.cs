using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class PaymentAuthorisation : Entity
    {
        [JsonConstructor]
        public PaymentAuthorisation() {}

        public string PaymentId { get; set; }
        public string PaymentStatus { get; set; }
        public bool Updated { get; set; }
        public string Error { get; set; }
        public int AuthStepsTaken { get; set; }
        public int AuthStepsRequired { get; set; }
        public string ShortReference { get; set; }


        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    PaymentId,
                    PaymentStatus,
                    Updated,
                    Error,
                    AuthStepsTaken,
                    AuthStepsRequired,
                    ShortReference
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PaymentAuthorisation))
            {
                return false;
            }

            var paymentAuthorisation = obj as PaymentAuthorisation;


            return PaymentId == paymentAuthorisation.PaymentId &&
                   PaymentStatus == paymentAuthorisation.PaymentStatus &&
                   Updated == paymentAuthorisation.Updated &&
                   Error == paymentAuthorisation.Error &&
                   AuthStepsTaken == paymentAuthorisation.AuthStepsTaken &&
                   AuthStepsRequired == paymentAuthorisation.AuthStepsRequired &&
                   ShortReference == paymentAuthorisation.ShortReference;

        }
        
        public override int GetHashCode()
        {
            return PaymentId.GetHashCode();
        }
    }
}