using System.Collections.Generic;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedBalances
    {
        internal PaginatedBalances() { }

        public List<Balance> Balances { get; set; }

        public Pagination Pagination { get; set; }
    }
}
