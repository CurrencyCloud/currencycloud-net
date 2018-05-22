using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedBalances
    {
        internal PaginatedBalances() { }

        public List<Balance> Balances { get; set; }

        public Pagination Pagination { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Balances,
                    Pagination
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}
