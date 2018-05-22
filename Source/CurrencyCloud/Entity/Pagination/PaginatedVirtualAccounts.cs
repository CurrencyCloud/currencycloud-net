using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedVirtualAccounts
    {
        internal PaginatedVirtualAccounts() { }

        public List<VirtualAccount> VirtualAccounts { get; set; }

        public Pagination Pagination { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    VirtualAccounts,
                    Pagination
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}