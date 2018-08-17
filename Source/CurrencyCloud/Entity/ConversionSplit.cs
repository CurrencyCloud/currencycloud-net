using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class ConversionSplit : Entity
    {
        [JsonConstructor]
        public ConversionSplit() { }

        public ConversionSplitDetails ParentConversion { get; set; }

        public ConversionSplitDetails ChildConversion { get; set; }

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

        public override bool Equals(object obj)
        {
            if (!(obj is ConversionSplit))
            {
                return false;
            }

            var conversionSplit = obj as ConversionSplit;

            return ParentConversion == conversionSplit.ParentConversion &&
                   ChildConversion == conversionSplit.ChildConversion;
        }

        public override int GetHashCode()
        {
            var hash = 11;
            hash = hash * 101 + ParentConversion.Id.GetHashCode();
            hash = hash * 101 + ChildConversion.Id.GetHashCode();
            return hash;
        }
    }
}
