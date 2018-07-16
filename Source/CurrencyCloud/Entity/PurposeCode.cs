using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class PaymentPurposeCode : Entity
    {
        internal PaymentPurposeCode() { }

        public string Currency { get; set; }

        public int EntityType { get; set; }

        public string PurposeCode { get; set; }

        public string PurposeDescription { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Currency,
                    EntityType,
                    PurposeCode,
                    PurposeDescription
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PaymentPurposeCode))
            {
                return false;
            }

            var pc = obj as PaymentPurposeCode;

            return Currency == pc.Currency &&
                   EntityType == pc.EntityType &&
                   PurposeCode == pc.PurposeCode &&
                   PurposeDescription == pc.PurposeDescription;
        }

        public override int GetHashCode()
        {
            return PurposeCode.GetHashCode();
        }
    }
}
