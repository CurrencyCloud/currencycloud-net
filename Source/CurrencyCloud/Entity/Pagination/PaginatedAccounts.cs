using System.Collections.Generic;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedAccounts
    {
        internal PaginatedAccounts() { }

        public List<Account> Accounts { get; set; }

        public Pagination Pagination { get; set; }
    }
}
