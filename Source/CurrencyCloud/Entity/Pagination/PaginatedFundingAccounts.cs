using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedFundingAccounts
    {
        internal PaginatedFundingAccounts() { }

        public List<FundingAccount> FundingAccounts { get; set; }

        public Pagination Pagination { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    FundingAccounts,
                    Pagination
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}