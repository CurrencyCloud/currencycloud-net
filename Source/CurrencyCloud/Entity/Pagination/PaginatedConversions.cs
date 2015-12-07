using System.Collections.Generic;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedConversions
    {
        internal PaginatedConversions() { }

        public List<Conversion> Conversions { get; set; }

        public Pagination Pagination { get; set; }
    }
}
