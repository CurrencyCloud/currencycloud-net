using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedReportRequests
    {
        internal PaginatedReportRequests() { }

        public List<ReportRequest> ReportRequests { get; set; }

        public Pagination Pagination { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    ReportRequests,
                    Pagination
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}