using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedPayments
    {
        internal PaginatedPayments() { }

        public List<Payment> Payments { get; set; }

        public Pagination Pagination { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Payments,
                    Pagination
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}
