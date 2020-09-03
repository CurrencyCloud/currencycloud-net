using Newtonsoft.Json;
using System.Collections.Generic;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedWithdrawalAccounts
    {
        internal PaginatedWithdrawalAccounts() { }

        public List<WithdrawalAccount> WithdrawalAccounts { get; set; }

        public Pagination Pagination { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    WithdrawalAccounts,
                    Pagination
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}