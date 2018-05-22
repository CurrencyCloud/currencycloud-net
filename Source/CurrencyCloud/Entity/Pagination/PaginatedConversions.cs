using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedConversions
    {
        internal PaginatedConversions() { }

        public List<Conversion> Conversions { get; set; }

        public Pagination Pagination { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Conversions,
                    Pagination
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}
