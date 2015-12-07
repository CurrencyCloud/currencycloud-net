using System.Collections.Generic;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedPayments
    {
        internal PaginatedPayments() { }

        public List<Payment> Payments { get; set; }

        public Pagination Pagination { get; set; }
    }
}
