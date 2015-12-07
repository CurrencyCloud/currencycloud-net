using System.Collections.Generic;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedTransactions
    {
        internal PaginatedTransactions() { }

        public List<Transaction> Transactions { get; set; }

        public Pagination Pagination { get; set; }
    }
}
