using System.Collections.Generic;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedBeneficiaries
    {
        internal PaginatedBeneficiaries() { }

        public List<Beneficiary> Beneficiaries { get; set; }

        public Pagination Pagination { get; set; }
    }
}
