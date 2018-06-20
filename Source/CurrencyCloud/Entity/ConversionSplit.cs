using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class ConversionSplit : Entity
    {

        [JsonConstructor]
        public ConversionSplit() { }

        public Conversion ParentConversion { get; set; }

        public Conversion ChildConversion { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    ParentConversion,
                    ChildConversion
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}
