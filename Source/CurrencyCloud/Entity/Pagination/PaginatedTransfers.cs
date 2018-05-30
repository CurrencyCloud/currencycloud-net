using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedTransfers
    {
        internal PaginatedTransfers() { }

        public List<Transfer> Transfers { get; set; }

        public Pagination Pagination { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Transfers,
                    Pagination
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}