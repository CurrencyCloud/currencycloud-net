using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedConversionProfitAndLosses
    {
        internal PaginatedConversionProfitAndLosses() { }

        public List<ConversionProfitAndLoss> ConversionProfitAndLosses { get; set; }

        public Pagination Pagination { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    ConversionProfitAndLosses,
                    Pagination
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}