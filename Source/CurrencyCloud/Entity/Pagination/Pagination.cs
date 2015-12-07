namespace CurrencyCloud.Entity.Pagination
{
    public class Pagination : Entity
    {
        public int TotalEntries { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage { get; set; }

        public int NextPage { get; set; }

        public int PerPage { get; set; }

        public string Order { get; set; }

        public string OrderAscDesc { get; set; }
    }
}
