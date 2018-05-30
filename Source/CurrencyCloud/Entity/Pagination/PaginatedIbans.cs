using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedIbans
    {
        internal PaginatedIbans() { }

        public List<Iban> Ibans { get; set; }

        public Pagination Pagination { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Ibans,
                    Pagination
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}