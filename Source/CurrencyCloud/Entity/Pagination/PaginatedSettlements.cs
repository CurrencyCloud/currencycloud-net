using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedSettlements
    {
        internal PaginatedSettlements() { }

        public List<Settlement> Settlements { get; set; }

        public Pagination Pagination { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Settlements,
                    Pagination
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}
