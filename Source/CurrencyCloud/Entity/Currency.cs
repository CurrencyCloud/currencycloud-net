using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class Currency : Entity
    {
        internal Currency() { }

        public string Code { get; set; }

        public int DecimalPlaces { get; set; }

        public string Name { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Code,
                    DecimalPlaces,
                    Name
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Currency))
            {
                return false;
            }

            var currency = obj as Currency;

            return Code == currency.Code &&
                   DecimalPlaces == currency.DecimalPlaces &&
                   Name == currency.Name;
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }
}
