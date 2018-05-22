using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedBeneficiaries
    {
        internal PaginatedBeneficiaries() { }

        public List<Beneficiary> Beneficiaries { get; set; }

        public Pagination Pagination { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Beneficiaries,
                    Pagination
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}
