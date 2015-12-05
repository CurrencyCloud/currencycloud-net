using System.Runtime.Serialization;

namespace CurrencyCloud.Entity.Page
{
    [DataContract]
    public class Pagination
    {
        [DataMember(Name = "total_entries")]
        public int TotalEntries { get; internal set; }

        [DataMember(Name = "total_pages")]
        public int TotalPages { get; internal set; }

        [DataMember(Name = "current_page")]
        public int CurrentPage { get; internal set; }

        [DataMember(Name = "previous_page")]
        public int PreviousPage { get; internal set; }

        [DataMember(Name = "next_page")]
        public int NextPage { get; internal set; }

        [DataMember(Name = "per_page")]
        public int PerPage { get; internal set; }

        [DataMember(Name = "order")]
        public string Order { get; internal set; }

        [DataMember(Name = "order_asc_desc")]
        public string OrderAscDesc { get; internal set; }
    }
}
