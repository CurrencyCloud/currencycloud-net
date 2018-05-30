using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedTransactions
    {
        internal PaginatedTransactions() { }

        public List<Transaction> Transactions { get; set; }

        public Pagination Pagination { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Transactions,
                    Pagination
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}
