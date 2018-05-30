using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedAccounts
    {
        internal PaginatedAccounts() { }

        public List<Account> Accounts { get; set; }

        public Pagination Pagination { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Accounts,
                    Pagination
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}
