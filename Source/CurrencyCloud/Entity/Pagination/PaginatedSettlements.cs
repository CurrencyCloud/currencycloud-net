using System.Collections.Generic;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedSettlements
    {
        internal PaginatedSettlements() { }

        public List<Settlement> Settlements { get; set; }

        public Pagination Pagination { get; set; }
    }
}
