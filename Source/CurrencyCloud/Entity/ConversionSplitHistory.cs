using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class ConversionSplitHistory : Entity
    {
        [JsonConstructor]
        public ConversionSplitHistory() { }

        public ConversionSplitHistory(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }

        public ConversionSplitDetails ParentConversion { get; set; }

        public ConversionSplitDetails OriginConversion { get; set; }

        public List<ConversionSplitDetails> ChildConversions { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    ParentConversion,
                    OriginConversion,
                    ChildConversions
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ConversionSplitHistory))
            {
                return false;
            }

            var conversionSplitHistory = obj as ConversionSplitHistory;

            return Id == conversionSplitHistory.Id &&
                   ParentConversion == conversionSplitHistory.ParentConversion &&
                   OriginConversion == conversionSplitHistory.OriginConversion &&
                   ChildConversions == conversionSplitHistory.ChildConversions;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}